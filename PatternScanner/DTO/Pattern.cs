using PatternScanner.DTO.Code;
using PatternScanner.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternScanner.DTO
{
    public class Pattern
    {
        public Group Group { get; set; }
        public bool[] Wildcards { get; private set; }
        public byte[] Bytes { get; private set; }

        public string Mask => string.Join("", Wildcards.Select(x => x ? "?" : "x").ToArray());
        public string BytesString => string.Join(" ", Bytes.Select(x => x.ToString("X2")).ToArray());

        public string Source { get; private set; }
        public string Name { get; set; }
        public ICodeParser Parser { get; private set; }

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

        public CodeText CodeText
        {
            get { return codeText; }
            set
            {
                if (value != codeText)
                {
                    codeText = value;
                    
                }
            }
        }
        private CodeText codeText;

        public Pattern(ICodeParser parser, string mask, byte[] bytes, string source, string name = null) : this(parser, mask.Select(x => x == '?' ? true : false).ToArray(), bytes, source, name) { }

        public Pattern(ICodeParser parser, bool[] wildcards, byte[] bytes, string source, string name = null)
        {
            Parser = parser;
            Wildcards = wildcards;
            Bytes = bytes;
            Source = source;
            Name = string.IsNullOrEmpty(name) ? HybridPattern : name;
            codeText = Parser.Parse(source);
            codeText.ApplyMask(Mask);
        }

        public static Pattern FromString(string pattern, string name)
        {
            var parts = pattern.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var bytes = parts.Select(x => (x == "??" || x == "?") ? (byte)0 : byte.Parse(x, System.Globalization.NumberStyles.HexNumber)).ToArray();
            var mask = string.Join("", parts.Select(x => (x == "??" || x == "?") ? "?" : "x").ToArray());

            return new Pattern(HexParser.Instance, mask, bytes, pattern, name);
        }
        public static Pattern FromParser(string source, string name, ICodeParser parser)
        {
            var codeText = parser.Parse(source);
            return new Pattern(parser, codeText.Mask, codeText.Bytes, source, name);
        }

        public override string ToString()
        {
            return HybridPattern;
        }

        public void ApplyMask(string mask)
        {
            ApplyMask(mask.Select(x => x == '?' ? true : false).ToArray());
        }
        public void ApplyMask(bool[] wildcards)
        {
            Array.Copy(wildcards, Wildcards, Wildcards.Length);
            CodeText.ApplyMask(wildcards);
        }
    }
}
