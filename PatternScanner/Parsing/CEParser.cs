using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PatternScanner.DTO;
using PatternScanner.DTO.Code;

namespace PatternScanner.Parsing
{
    class CEParser : ICodeParser
    {
        private static Regex REGEX_CE = new Regex("([^\\s]+)\\s-(\\s[0-9A-F]{2,8})+\\s+-\\s([^\n]*)", RegexOptions.Multiline | RegexOptions.Compiled);

        public static CEParser Instance => new CEParser();
        private CEParser() { }

        public CodeText Parse(string text)
        {
            var matches = REGEX_CE.Matches(text);
            var codeRows = new List<CodeRow>();
            foreach (var match in matches.Cast<Match>())
            {
                var codeBytes = new List<CodeByte>();
                var section = match.Groups[1].Value;
                var bytes = string.Join("", match.Groups[2].Captures.Cast<Capture>().Select(x => x.Value.Trim()).ToArray());
                var asm = match.Groups[3].Value.Trim();
                var singleBytes = new List<byte>();
                for (int i = 0; i < bytes.Length; i += 2)
                    singleBytes.Add(byte.Parse(bytes.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
                codeRows.Add(new CodeRow(singleBytes.Select(x => new CodeByte(x)).ToArray(), asm));
            }
            return new CodeText(codeRows.ToArray());
        }
    }
}
