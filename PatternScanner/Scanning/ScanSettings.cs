using PatternScanner.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternScanner.Scanning
{
    public class ScanSettings
    {
        public long Address { get; private set; }
        public long Size { get; private set; }
        public Pattern Pattern { get; private set; }

        public ScanSettings(long address, long size, Pattern pattern)
        {
            Address = address;
            Size = size;
            Pattern = pattern;
        }

        public ScanSettings[] Split(int count)
        {
            if (count <= 0)
                throw new Exception("Invalid count of splits");

            if (count == 1)
                return new ScanSettings[] { this };

            var settings = new ScanSettings[count];
            var size = Size / count;
            for (int i = 0; i < settings.Length; i++)
                settings[i] = new ScanSettings(Address + size * i, size, Pattern);
            return settings;
        }
    }
}
