using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternScanner.Scanning
{
    public interface IScanner
    {
        Task<ScanResult[]> PerformScan(Stream input, ScanSettings settings, ScanProgress progress);
        void Cancel();
    }
}
