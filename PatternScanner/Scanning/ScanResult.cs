using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternScanner.Scanning
{
    public class ScanResult
    {
        public long Address { get; private set; }
        public byte[] Bytes { get; private set; }
        
        public ScanResult(long address, byte[] bytes)
        {
            Address = address;
            Bytes = bytes;
        }
    }
}
