using System;
using System.Collections.Generic;
using System.Linq;

namespace PatternScanner.DTO
{
    public class CodeRow : IEquatable<CodeRow>
    {
        public CodeByte[] Bytes { get; private set; }
        public string Text { get; private set; }

        public byte[] AllBytes => Bytes.Select(x => (byte)x.Value).ToArray();
        public string ByteString => string.Join("", Bytes.Select(x => $"\\0x{x.Value.ToString("X2")}").ToArray());
        public string MaskString => string.Join("", Bytes.Select(x => x.Wildcard ? "?" : "x").ToArray());

        public event EventHandler PatternChanged;

        public CodeRow(CodeByte[] bytes, string text)
        {
            Bytes = bytes;
            Text = text;
            foreach (var b in bytes)
                b.WildcardChanged += (o, e) => PatternChanged?.Invoke(this, EventArgs.Empty);
        }

        public override bool Equals(object obj)
        {
            if (obj is CodeRow)
                return obj.GetHashCode() == GetHashCode();
            return false;
        }

        public bool Equals(CodeRow other)
        {
            return other != null &&
                   EqualityComparer<CodeByte[]>.Default.Equals(Bytes, other.Bytes) &&
                   Text == other.Text &&
                   ByteString == other.ByteString &&
                   MaskString == other.MaskString;
        }

        public override int GetHashCode()
        {
            var hashCode = 1572661728;
            hashCode = hashCode * -1521134295 + EqualityComparer<CodeByte[]>.Default.GetHashCode(Bytes);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ByteString);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(MaskString);
            return hashCode;
        }
    }
}
