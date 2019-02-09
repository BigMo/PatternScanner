using PatternScanner.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PatternScanner.Scanning
{
    public class Scanner : IScanner
    {
        private CancellationTokenSource token;
        private static object readLock = new object();

        public void Cancel()
        {
            token.Cancel();
        }

        public async Task<ScanResult[]> PerformScan(Stream input, ScanSettings settings, ScanProgress progress)
        {
            token = new CancellationTokenSource();
            var results = await Task.Factory.StartNew<ScanResult[]>(() =>
            {
                var _results = new List<ScanResult>();
                var buffer = new byte[4096];
                var address = settings.Address;
                var bytesLeft = settings.Size;
                var bytesRead = 0;

                while (bytesLeft > 0 && !token.IsCancellationRequested)
                {
                    lock (readLock)
                    {
                        input.Position = address;
                        bytesRead = input.Read(buffer, 0, (int)Math.Min(buffer.Length, bytesLeft));
                    }
                    bytesLeft -= bytesRead;

                    if (bytesRead > 0)
                    {
                        for(int i = 0; i < bytesRead - settings.Pattern.Bytes.Length && !token.IsCancellationRequested; i++)
                        {
                            if (Matches(buffer, i, settings.Pattern))
                            {
                                var resultBuffer = new byte[settings.Pattern.Bytes.Length];
                                Array.Copy(buffer, i, resultBuffer, 0, resultBuffer.Length);
                                _results.Add(new ScanResult(address + i, resultBuffer));
                                progress.Found();
                            }
                        }
                    }

                    address += bytesRead;
                    progress.Progress(bytesRead);
                }

                return _results.ToArray();
            });
            return results;
        }

        private bool Matches(byte[] buffer, int index, Pattern pattern)
        {
            for (int i = 0; i < pattern.Bytes.Length; i++)
                if (!pattern.Wildcards[i] && buffer[index + i] != pattern.Bytes[i])
                    return false;

            return true;
        }
    }
}
