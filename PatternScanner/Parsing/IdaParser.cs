using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PatternScanner.DTO;

namespace PatternScanner.Parsing
{
    class IDAParser : ICodeParser
    {
        private static Regex REGEX_IDA = new Regex("\\.([^:]*):([0-9A-F]+)(\\s[0-9A-F]{2})+\\s+([^\n]*)", RegexOptions.Multiline | RegexOptions.Compiled);

        public static IDAParser Instance => new IDAParser();
        private IDAParser() { }
                
        public CodeText Parse(string text)
        {
            var matches = REGEX_IDA.Matches(text);
            var codeRows = new List<CodeRow>();
            foreach (var match in matches.Cast<Match>())
            {
                var codeBytes = new List<CodeByte>();
                var section = match.Groups[1].Value;
                var address = match.Groups[2].Value;
                var bytes = match.Groups[3].Captures.Cast<Capture>().Select(x => x.Value).ToArray();
                var asm = match.Groups[4].Value.Trim();
                codeRows.Add(new CodeRow(bytes.Select(x => new CodeByte(int.Parse(x.Trim(), System.Globalization.NumberStyles.HexNumber))).ToArray(), asm));
            }
            return new CodeText(codeRows.ToArray());
        }
    }
}
