using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PatternScanner.DTO
{
    public class CodeText : IEquatable<CodeText>
    {
        public CodeRow[] Rows { get; private set; }

        public byte[] AllBytes => Rows.SelectMany(x => x.AllBytes).ToArray();
        public string ByteString => string.Join("", Rows.Select(x => x.ByteString).ToArray());
        public string MaskString => string.Join("", Rows.Select(x => x.MaskString).ToArray());

        public event EventHandler PatternChanged;

        public CodeText(CodeRow[] rows)
        {
            Rows = rows;
            foreach (var r in rows)
                r.PatternChanged += (o, e) => PatternChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ApplyMask(string mask)
        {
            if (mask == MaskString)
                return;

            var allBytes = Rows.SelectMany(x => x.Bytes).ToArray();
            if (allBytes.Length != mask.Length)
                throw new Exception($"Length missmatch: mask ({mask.Length}), bytes ({allBytes.Length})");

            for (int i = 0; i < allBytes.Length; i++)
                allBytes[i].Wildcard = mask[i] == '?';
        }

        public override bool Equals(object obj)
        {
            if (obj is CodeText)
                return obj.GetHashCode() == GetHashCode();
            return false;
        }
        public bool Equals(CodeText other)
        {
            return other != null &&
                   EqualityComparer<CodeRow[]>.Default.Equals(Rows, other.Rows) &&
                   ByteString == other.ByteString &&
                   MaskString == other.MaskString;
        }
        public override int GetHashCode()
        {
            var hashCode = 1814035286;
            hashCode = hashCode * -1521134295 + EqualityComparer<CodeRow[]>.Default.GetHashCode(Rows);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ByteString);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MaskString);
            return hashCode;
        }
    }
}
