using PatternScanner.DTO;
using PatternScanner.PeExtension;
using PeNet;
using System;
using System.Collections.Generic;
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
        private Task<ListViewItem[]>[] scanTasks;
        private CancellationTokenSource cancellation;
        private ScanProgress progress;

        private struct ScanSettings
        {
            public long address, size;
            public byte[] bytes;
            public string mask;
        }
        private class ScanProgress
        {
            public ScanSettings[] Settings { get; private set; }
            public long[] BytesRead { get; private set; }
            public int Findings { get; private set; }
            public int lastUpdate;

            public event EventHandler MadeProgress, FoundItems;

            public void Setup(ScanSettings[] settings)
            {
                Settings = settings;
                BytesRead = new long[settings.Length];
                Findings = 0;
                lastUpdate = 0;
            }

            public void Update(int id, long bytes)
            {
                BytesRead[id] = bytes;
                MadeProgress?.Invoke(this, EventArgs.Empty);
            }
            public void AddFinding()
            {
                Findings++;
                FoundItems?.Invoke(this, EventArgs.Empty);
            }
        }

        public frmMain()
        {
            InitializeComponent();
            Icon = Properties.Resources.Logo_256;

            progress = new ScanProgress();
            progress.MadeProgress += (o, e) =>
            {
                var totalBytes = progress.BytesRead.Sum();
                var perc = (int)(100 * (totalBytes / (double)progress.Settings.Sum(x => x.size)));
                if (perc > progress.lastUpdate)
                {
                    progress.lastUpdate = perc;
                    this.Invoke((MethodInvoker)delegate { progressBar1.Value = Math.Min(perc, progressBar1.Maximum); });
                }
            };
            progress.FoundItems += (o, e) =>
            {
                if (progress.Findings < 10 ||
                    progress.Findings < 100 && progress.Findings % 10 == 0 ||
                    progress.Findings < 1000 && progress.Findings % 100 == 0 ||
                    progress.Findings < 10000 && progress.Findings % 1000 == 0 ||
                    progress.Findings < 100000 && progress.Findings % 1000 == 0 ||
                    progress.Findings < 1000000 && progress.Findings % 10000 == 0)
                    this.Invoke((MethodInvoker)delegate { lblOccurences.Text = progress.Findings.ToString(); });
            };
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
            txbBytes.Text = patternTable.CodeText.ByteString;
            txbMask.Text = patternTable.CodeText.MaskString;
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
            var mask = patternTable.CodeText.MaskString;
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
            if (scanTasks == null)
            {
                var section = cbbSection.SelectedItem as ImageSection;
                var scanSettings = new ScanSettings()
                {
                    address = section != null ? section.Address : 0,
                    size = section != null ? section.Size : file.Length,
                    bytes = patternTable.CodeText.AllBytes,
                    mask = patternTable.CodeText.MaskString
                };
                cancellation = new CancellationTokenSource();
                scanTasks = new Task<ListViewItem[]>[Environment.ProcessorCount/* / 2*/];
                var scanTaskSettings = new ScanSettings[scanTasks.Length];
                for (int i = 0; i < scanTaskSettings.Length; i++)
                {
                    scanTaskSettings[i] = new ScanSettings()
                    {
                        address = scanSettings.address + (scanSettings.size / scanTasks.Length)*i,
                        size = scanSettings.size / scanTasks.Length,
                        bytes = scanSettings.bytes,
                        mask = scanSettings.mask
                    };
                    
                }
                progress.Setup(scanTaskSettings);
                for (int i = 0; i < scanTasks.Length; i++)
                {
                    int a = i;
                    var _settings = scanTaskSettings[a];
                    scanTasks[i] = new Task<ListViewItem[]>(() => Scan(a, _settings, progress));
                    scanTasks[i].Start();
                }
                btnScan.Text = "Cancel";
                progressBar1.Visible = true;
                ltvOccurences.Items.Clear();
                ltvOccurences.SuspendLayout();
                progressBar1.Visible = true;

                var items = await Task.WhenAll(scanTasks);
                var allItems = items.SelectMany(x => x).OrderBy(x => x.Text).ToArray();
                ltvOccurences.Items.AddRange(allItems);
                ltvOccurences.ResumeLayout();
                lblOccurences.Text = allItems.Length.ToString();
                btnScan.Text = "Scan";
                progressBar1.Visible = false;
                scanTasks = null;
            }
            else
            {
                cancellation.Cancel();
            }
        }

        private ListViewItem[] Scan(int index, ScanSettings settings, ScanProgress progress)
        {
            var items = new List<ListViewItem>();
            using (var str = file.OpenRead())
            {
                str.Position = settings.address;
                byte[] buffer = new byte[Math.Min(1024 * 1024, settings.size)];
                do
                {
                    var pos = str.Position;
                    var count = str.Read(buffer, 0, buffer.Length);
                    if (count == 0)
                        break;

                    for (int i = 0; i < count - settings.mask.Length && !cancellation.IsCancellationRequested; i++)
                    {
                        if (Matches(buffer, i, settings.bytes, settings.mask))
                        {
                            var ltv = new ListViewItem((pos + i).ToString("X8"));
                            ltv.SubItems.Add(string.Join(" ", buffer.Skip(i).Take(settings.mask.Length).Select(x => x.ToString("X2")).ToArray()));
                            items.Add(ltv);
                            progress.AddFinding();
                        }
                        //if (i % 1024 == 0)
                        //    progress.Update(index, (settings.address - pos) + i);
                    }
                    progress.Update(index, str.Position - settings.address);
                } while (
                    !cancellation.IsCancellationRequested &&
                    str.Position < str.Length &&
                    str.Position < settings.address + settings.size);
            }

            return items.ToArray();
        }

        private bool Matches(byte[] data, int index, byte[] bytes, string mask)
        {
            for (int i = 0; i < mask.Length; i++)
                if (mask[i] != '?' && data[index + i] != bytes[i])
                    return false;

            return true;
        }
    }
}
