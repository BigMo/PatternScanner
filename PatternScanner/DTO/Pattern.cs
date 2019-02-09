using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternScanner.DTO
{
    public class Pattern
    {
        public bool[] Wildcards { get; private set; }
        public byte[] Bytes { get; private set; }

        public string Mask => string.Join("", Wildcards.Select(x => x ? "?" : "x").ToArray());
        public string BytesString => string.Join(" ", Bytes.Select(x => x.ToString("X2")).ToArray());

        public string HybridPattern
        {
            get
            {
                string[] parts = new string[Bytes.Length];
                for (int i = 0; i < Wildcards.Length; i++)
                    parts[i] = Wildcards[i] ? "?" : Bytes[i].ToString("X2");
                return string.Join(" ", parts);
            }
        }

        public Pattern(string mask, byte[] bytes) : this(mask.Select(x => x == '?' ? true : false).ToArray(), bytes) { }

        public Pattern(bool[] wildcards, byte[] bytes)
        {
            Wildcards = wildcards;
            Bytes = bytes;
        }

        public static Pattern FromString(string pattern)
        {
            var parts = pattern.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var bytes = parts.Select(x => (x == "??" || x == "?") ? (byte)0 : byte.Parse(x, System.Globalization.NumberStyles.HexNumber)).ToArray();
            var mask = string.Join(" ", parts.Select(x => (x == "??" || x == "?") ? "?" : "x").ToArray());

            return new Pattern(mask, bytes);
        }

        public override string ToString()
        {
            return HybridPattern;
        }
    }
}
