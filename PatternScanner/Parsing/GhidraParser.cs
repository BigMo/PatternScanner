using PatternScanner.DTO.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PatternScanner.Parsing
{
    class GhidraParser : ICodeParser
    {
        //Full row
        //private static Regex REGEX_GHIDRA = new Regex("\\s+([0-9A-Fa-f]+)(( [0-9A-Fa-f]{2})+)\\s+([^\n]*)", RegexOptions.Multiline | RegexOptions.Compiled);
        //Partial row
        //(( [0-9A-Fa-f]{2})+)
        private static Regex REGEX_GHIDRA_FULL = new Regex("(\\s+([0-9A-Fa-f]{8,16})(( [0-9A-Fa-f]{2})+)\\s+([^\n]*))|(( [0-9A-Fa-f]{2})+)", RegexOptions.Multiline | RegexOptions.Compiled);

        public static GhidraParser Instance => new GhidraParser();
        private GhidraParser() { }

        public CodeText Parse(string text)
        {
            var matches = REGEX_GHIDRA_FULL.Matches(text);
            var codeRows = new List<CodeRow>();
            foreach (var match in matches.Cast<Match>())
            {
                if (match.Groups[6].Value != "")
                {
                    var bytes = match.Groups[6].Value.Trim().Split(' ');
                    codeRows.Add(new CodeRow(bytes.Select(x => new CodeByte(int.Parse(x.Trim(), System.Globalization.NumberStyles.HexNumber))).ToArray(),""));
                }
                else
                {
                    var address = match.Groups[2].Value;
                    var bytes = match.Groups[3].Value.Trim().Split(' ');
                    var asm = match.Groups[5].Value.Trim();
                    codeRows.Add(new CodeRow(bytes.Select(x => new CodeByte(int.Parse(x.Trim(), System.Globalization.NumberStyles.HexNumber))).ToArray(), asm));
                }
            }
            return new CodeText(codeRows.ToArray());
        }
    }
}
