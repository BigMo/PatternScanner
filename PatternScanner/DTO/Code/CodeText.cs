using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PatternScanner.DTO.Code
{
    public class CodeText
    {
        public CodeRow[] Rows { get; private set; }
        public string Mask => string.Join("", Rows.Select(x => x.MaskString).ToArray());
        public byte[] Bytes => Rows.SelectMany(x => x.AllBytes).ToArray();

        public event EventHandler PatternChanged;

        public CodeText(CodeRow[] rows)
        {
            Rows = rows;
            foreach (var r in rows)
                r.PatternChanged += (o, e) => PatternChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ApplyMask(string mask)
        {
            ApplyMask(mask.Select(x => x == '?' ? true : false).ToArray());
        }

        public void ApplyMask(bool[] wildcards)
        {
            var allBytes = Rows.SelectMany(x => x.Bytes).ToArray();
            if (allBytes.Length != wildcards.Length)
                throw new Exception($"Length missmatch: wildcards ({wildcards.Length}), bytes ({allBytes.Length})");

            for (int i = 0; i < allBytes.Length; i++)
                allBytes[i].Wildcard = wildcards[i];
        }
    }
}
