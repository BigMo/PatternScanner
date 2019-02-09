using PatternScanner.DTO;
using PatternScanner.Extensions;
using PatternScanner.PeExtension;
using PatternScanner.Scanning;
using PeNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatternScanner.UI
{
    public partial class frmMain : Form
    {
        public FileInfo File
        {
            get { return file; }
            set
            {
                if (file != value)
                {
                    file = value;
                    ltvOccurences.Items.Clear();
                    cbbSection.SelectedItem = null;
                    cbbSection.DataSource = null;
                    if (file != null && file.Exists)
                    {
                        LoadPeFile();
                        lblFile.Text = file.Name;
                        btnScan.Enabled = true;
                    }
                    else
                    {
                        lblFile.Text = "-";
                        btnScan.Enabled = false;
                    }
                }
            }
        }
        private FileInfo file;
        private PeFile peFile;
        private MultithreadedScanner scanner;
        //private Task<ListViewItem[]>[] scanTasks;
        //private CancellationTokenSource cancellation;
        //private ScanProgress progress;

        //private struct ScanSettings
        //{
        //    public long address, size;
        //    public byte[] bytes;
        //    public string mask;
        //}
        ////TODO: Implement better progress synchronization.
        //private class ScanProgress
        //{
        //    public ScanSettings[] Settings { get; private set; }
        //    public long[] BytesRead { get; private set; }
        //    public int Findings { get; private set; }
        //    public int lastUpdate;

        //    public event EventHandler MadeProgress, FoundItems;

        //    public void Setup(ScanSettings[] settings)
        //    {
        //        Settings = settings;
        //        BytesRead = new long[settings.Length];
        //        Findings = 0;
        //        lastUpdate = 0;
        //    }

        //    public void Update(int id, long bytes)
        //    {
        //        BytesRead[id] = bytes;
        //        MadeProgress?.Invoke(this, EventArgs.Empty);
        //    }
        //    public void AddFinding()
        //    {
        //        Findings++;
        //        FoundItems?.Invoke(this, EventArgs.Empty);
        //    }
        //}

        public frmMain()
        {
            InitializeComponent();
            Icon = Properties.Resources.Logo_256;

            //progress = new ScanProgress();
            //progress.MadeProgress += (o, e) =>
            //{
            //    var totalBytes = progress.BytesRead.Sum();
            //    var perc = (int)(100 * (totalBytes / (double)progress.Settings.Sum(x => x.size)));
            //    if (perc > progress.lastUpdate)
            //    {
            //        progress.lastUpdate = perc;
            //        this.Invoke((MethodInvoker)delegate { progressBar1.Value = Math.Min(perc, progressBar1.Maximum); });
            //    }
            //};
            //progress.FoundItems += (o, e) =>
            //{
            //    if (progress.Findings < 10 ||
            //        progress.Findings < 100 && progress.Findings % 10 == 0 ||
            //        progress.Findings < 1000 && progress.Findings % 100 == 0 ||
            //        progress.Findings < 10000 && progress.Findings % 1000 == 0 ||
            //        progress.Findings < 100000 && progress.Findings % 1000 == 0 ||
            //        progress.Findings < 1000000 && progress.Findings % 10000 == 0)
            //        this.Invoke((MethodInvoker)delegate { lblOccurences.Text = progress.Findings.ToString(); });
            //};
        }

        private void LoadPeFile()
        {
            try
            {
                peFile = new PeFile(System.IO.File.ReadAllBytes(file.FullName));
                var sections = peFile.ImageSectionHeaders.Select(x => new ImageSection(peFile, x)).ToArray();
                if (sections.Length > 0)
                {
                    cbbSection.DataSource = sections;
                    cbbSection.SelectedItem = cbbSection.Items[0];
                }
            }
            catch { }
        }

        private void patternTable_PatternChanged(object sender, EventArgs e)
        {
            var pattern = patternTable.CodeText.Pattern;
            txbBytes.Text = pattern.BytesString;
            txbMask.Text = pattern.Mask;
            txbPattern.Text = pattern.HybridPattern;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (var frm = new frmPatternImport())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    patternTable.CodeText = frm.CodeText;
                }
            }
        }
        private void txbMask_TextChanged(object sender, EventArgs e)
        {
            var mask = patternTable.CodeText.Pattern.Mask;
            try
            {
                patternTable.CodeText.ApplyMask(txbMask.Text);
            }
            catch
            {
                txbMask.Text = mask;
            }
        }
        private void btnFile_Click(object sender, EventArgs e)
        {
            using (var diag = new OpenFileDialog())
            {
                if (diag.ShowDialog() == DialogResult.OK)
                {
                    File = new FileInfo(diag.FileName);
                }
            }
        }

        private async void btnScan_Click(object sender, EventArgs e)
        {
            if (scanner == null)
            {
                var section = cbbSection.SelectedItem as ImageSection;
                scanner = new MultithreadedScanner();
                var settings = new ScanSettings(
                    section != null ? section.Address : 0,
                    section != null ? section.Size : file.Length,
                    patternTable.CodeText.Pattern);

                var progress = new ScanProgress(settings);
                progress.MadeProgressPercent += (_o, _e) => this.Invoke((MethodInvoker)delegate { progressBar1.Value = progress.ProgressPercent; });
                progress.PatternFound += (_o, _e) =>
                {
                    if (progress.Findings < 10 ||
                    (progress.Findings < 100 && progress.Findings % 10 == 0) ||
                    (progress.Findings < 1000 && progress.Findings % 100 == 0) ||
                    (progress.Findings < 10000 && progress.Findings % 1000 == 0) ||
                    (progress.Findings < 100000 && progress.Findings % 10000 == 0) ||
                        progress.Findings % 100000 == 0)
                        this.Invoke((MethodInvoker)delegate { this.lblOccurences.Text = progress.Findings.ToString(); });
                };

                btnScan.Text = "Cancel";
                progressBar1.Visible = true;
                //TODO: Make the listview use virtual mode. Populating/clearing the listview takes up the most time right now.
                ltvOccurences.SuspendLayout();
                ltvOccurences.Items.Clear();
                ltvOccurences.ResumeLayout();

                var results = await scanner.PerformScan(file.OpenRead(), settings, progress);

                lblOccurences.Text = results.Length.ToString();
                var ltvs = new ListViewItem[results.Length];
                for (int i = 0; i < results.Length; i++)
                {
                    ltvs[i] = new ListViewItem(results[i].Address.ToString("X8"));
                    ltvs[i].SubItems.Add(string.Join(" ", results[i].Bytes.Select(x => x.ToString("X2")).ToArray()));
                }
                ltvOccurences.SuspendLayout();
                ltvOccurences.Items.AddRange(ltvs);
                ltvOccurences.ResumeLayout();

                btnScan.Text = "Scan";
                progressBar1.Visible = false;
                scanner = null;
            }
            else
            {
                scanner.Cancel();
            }
        }

        private void lnkUC_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.unknowncheats.me/forum/members/562321.html");
        }

        private void lnkGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/BigMo/PatternScanner");
        }
    }
}
