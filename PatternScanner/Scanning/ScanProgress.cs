using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternScanner.Scanning
{
    public class ScanProgress
    {
        private object findingLock, progressLock;

        public int Findings { get; private set; }
        public int BytesScanned { get; private set; }
        public int ProgressPercent { get; private set; }
        public ScanSettings Settings { get; private set; }

        public event EventHandler PatternFound;
        public event EventHandler MadeProgress;
        public event EventHandler MadeProgressPercent;

        public ScanProgress(ScanSettings settings)
        {
            findingLock = new object();
            progressLock = new object();
            Settings = settings;
            Findings = 0;
            BytesScanned = 0;
            ProgressPercent = 0;
        }

        public void Found()
        {
            lock (findingLock)
            {
                Findings++;
                PatternFound?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Progress(int size)
        {
            lock (progressLock)
            {
                BytesScanned += size;
                MadeProgress?.Invoke(this, EventArgs.Empty);

                var percent = (int)Math.Min(100, Math.Max(0, 100*((double)BytesScanned / (double)Settings.Size)));
                if (percent > ProgressPercent)
                {
                    ProgressPercent = percent;
                    MadeProgressPercent?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}
