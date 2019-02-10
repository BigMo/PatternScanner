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
        private int findings;

        public int Findings => findings;
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
            findings = 0;
            BytesScanned = 0;
            ProgressPercent = 0;
        }

        public void Found()
        {
            findings++;
            if (findings < 10 ||
                (findings < 100 && findings % 10 == 0) ||
                (findings < 1000 && findings % 100 == 0) ||
                (findings < 10000 && findings % 1000 == 0) ||
                (findings < 100000 && findings % 10000 == 0) ||
                    findings % 100000 == 0)
            {
                lock (findingLock)
                {
                    PatternFound?.Invoke(this, EventArgs.Empty);
                }
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
