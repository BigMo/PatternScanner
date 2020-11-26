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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

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
        private ScanResult[] lastResults = new ScanResult[0];

        public frmMain()
        {
            InitializeComponent();
            Icon = Properties.Resources.Logo_256;
            var proj = new ProjectView();
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
            var pattern = patternTable.Pattern;
            txbBytes.Text = pattern.BytesString;
            txbMask.Text = pattern.Mask;
            txbPattern.Text = pattern.HybridPattern;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            using (var frm = new frmPatternImport())
            {
                frm.Pattern = patternTable.Pattern;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    patternTable.Pattern = frm.Pattern;
                }
            }
        }
        private void txbMask_TextChanged(object sender, EventArgs e)
        {
            var mask = patternTable.Pattern.Mask;
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

        private Task ClearResults()
        {
            return Task.Factory.StartNew(() =>
            {
                lastResults = new ScanResult[0];
                this.Invoke((MethodInvoker)delegate
                {
                    ltvOccurences.BeginUpdate();
                    ltvOccurences.VirtualListSize = 0;
                    ltvOccurences.EndUpdate();
                    lblOccurences.Text = "-";
                    lblTime.Text = "-";
                });
                GC.Collect();
                GC.WaitForPendingFinalizers();
            });
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
                    patternTable.Pattern);

                var progress = new ScanProgress(settings);
                progress.MadeProgressPercent += (_o, _e) => 
                    this.Invoke((MethodInvoker)delegate { progressBar1.Value = progress.ProgressPercent; });
                progress.PatternFound += (_o, _e) => 
                    this.Invoke((MethodInvoker)delegate { this.lblOccurences.Text = progress.Findings.ToString("N0"); });

                var watch = new Stopwatch();
                watch.Start();
                await ClearResults();
                btnScan.Text = "Cancel";
                progressBar1.Visible = true;
                lastResults = await scanner.PerformScan(file.OpenRead(), settings, progress);
                watch.Stop();

                lblOccurences.Text = lastResults.Length.ToString("N0");
                lblTime.Text = watch.Elapsed.ToString(@"hh\:mm\:ss\.fff");
                ltvOccurences.BeginUpdate();
                ltvOccurences.VirtualListSize = lastResults.Length;
                ltvOccurences.EndUpdate();

                btnScan.Text = "Scan";
                progressBar1.Visible = false;
                scanner = null;
            }
            else
            {
                scanner.Cancel();
            }
        }

        private void ltvOccurences_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            e.Item = null;
            if (e.ItemIndex < lastResults.Length)
            {
                e.Item = new ListViewItem(lastResults[e.ItemIndex].Address.ToString("X8"));
                e.Item.SubItems.Add(string.Join(" ", lastResults[e.ItemIndex].Bytes.Select(x => x.ToString("X2")).ToArray()));
                e.Item.Tag = lastResults[e.ItemIndex];
            }
        }

        private void ctxOccurences_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var items = ltvOccurences.SelectedIndices.Cast<int>();

            cpyAddress.Enabled = cpyBytes.Enabled = cpyAddressBytes.Enabled = items.Any();
            clearAll.Enabled = lastResults.Length > 0;
        }

        private async void clearAll_Click(object sender, EventArgs e)
        {
            await ClearResults();
        }
        
        private void CopyToClipboard(IEnumerable<ScanResult> results, bool copyAddress, bool copyBytes)
        {
            var builder = new StringBuilder();
            foreach(var res in results)
            {
                if (copyAddress)
                    builder.Append(res.Address.ToString("X8"));

                if (copyBytes) {
                    if (copyAddress)
                        builder.Append(": ");
                    builder.Append(string.Join(" ", res.Bytes.Select(x => x.ToString("X2")).ToArray()));
                }
                builder.AppendLine();
            }
            Clipboard.SetText(builder.ToString().Trim());
        }

        private void cpyAddress_Click(object sender, EventArgs e)
        {
            var items = ltvOccurences.SelectedIndices.Cast<int>();
            var results = items.Select(x => lastResults[x]);
            CopyToClipboard(results, true, false);
        }

        private void cpyBytes_Click(object sender, EventArgs e)
        {
            var items = ltvOccurences.SelectedIndices.Cast<int>();
            var results = items.Select(x => lastResults[x]);
            CopyToClipboard(results, false, true);
        }

        private void cpyAddressBytes_Click(object sender, EventArgs e)
        {
            var items = ltvOccurences.SelectedIndices.Cast<int>();
            var results = items.Select(x => lastResults[x]);
            CopyToClipboard(results, true, true);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var frm = new frmAbout())
                frm.ShowDialog();
        }

        private void projectView1_SelectedPatternChanged(object sender, EventArgs e)
        {
            patternTable.Pattern = projectView1.SelectedPattern;
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Filter = "PatternScanner-Project (*.psproj)|*.psproj";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    var xml = new XmlSerializer(typeof(Project)/*, new Type[] { typeof(Group), typeof(Pattern) }*/);
                    using (var writer = new StreamWriter(sfd.FileName))
                    {
                        xml.Serialize(writer, projectView1.Project);
                    }
                }
            }
        }
    }
}
