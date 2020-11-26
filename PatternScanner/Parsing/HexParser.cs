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
    public class HexParser : ICodeParser
    {
        private static Regex REGEX_HEX = new Regex("([0-9A-Fa-f]{2}|(\\?\\?|\\?))", RegexOptions.Compiled | RegexOptions.Multiline);

        public static HexParser Instance => new HexParser();
        private HexParser() { }
        
        public CodeText Parse(string text)
        {
            var matches = REGEX_HEX.Matches(text).Cast<Match>().ToArray();
            var codeRows = new List<CodeRow>();

            for (int i = 0; i < matches.Length; i += 8)
            {
                var num = Math.Min(matches.Length - i, 8);
                var _matches = matches.Skip(i).Take(num).ToArray();
                var bytes = new CodeByte[num];
                for(int m = 0; m < num; m++)
                {
                    if (_matches[m].Groups[1].Value.Contains("?"))
                        bytes[m] = new CodeByte(0) { Wildcard = true };
                    else
                        bytes[m] = new CodeByte(byte.Parse(_matches[m].Groups[1].Value.Trim(), System.Globalization.NumberStyles.HexNumber));
                }
                var asm = string.Join(" ", bytes.Select(x => x.Wildcard ? "??" : x.Value.ToString("X2")).ToArray());
                codeRows.Add(new CodeRow(bytes, asm));
            }
            return new CodeText(codeRows.ToArray());
        }
    }
}
