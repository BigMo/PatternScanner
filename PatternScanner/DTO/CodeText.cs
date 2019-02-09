using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PatternScanner.DTO
{
    public class CodeText
    {
        public CodeRow[] Rows { get; private set; }

        public Pattern Pattern =>
            new Pattern(
                string.Join("", Rows.Select(x => x.MaskString).ToArray()),
                Rows.SelectMany(x => x.AllBytes).ToArray()
                );

        public event EventHandler PatternChanged;

        public CodeText(CodeRow[] rows)
        {
            Rows = rows;
            foreach (var r in rows)
                r.PatternChanged += (o, e) => PatternChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ApplyMask(string mask)
        {
            if (mask == Pattern.Mask)
                return;

            var allBytes = Rows.SelectMany(x => x.Bytes).ToArray();
            if (allBytes.Length != mask.Length)
                throw new Exception($"Length missmatch: mask ({mask.Length}), bytes ({allBytes.Length})");

            for (int i = 0; i < allBytes.Length; i++)
                allBytes[i].Wildcard = mask[i] == '?';
        }
    }
}
