using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatternScanner.Scanning
{
    public class MultithreadedScanner : IScanner
    {
        private Scanner[] scanners;

        public int Threads { get; set; }

        public MultithreadedScanner() : this(Environment.ProcessorCount) { }

        public MultithreadedScanner(int threads)
        {
            Threads = threads;
        }

        public void Cancel()
        {
            foreach (var s in scanners)
                s.Cancel();
        }

        public async Task<ScanResult[]> PerformScan(Stream input, ScanSettings settings, ScanProgress progress)
        {
            var _settings = settings.Split(Threads);
            scanners = Enumerable.Range(0, Threads).Select(x => new Scanner()).ToArray();
            var tasks = new Task<ScanResult[]>[Threads];
            for (int i = 0; i < tasks.Length; i++)
                tasks[i] = scanners[i].PerformScan(input, _settings[i], progress);

            var results = await Task.WhenAll(tasks);
            return results.SelectMany(x => x).ToArray();
        }
    }
}
