using System;

namespace PatternScanner.DTO.Code
{
    public class CodeByte : IEquatable<CodeByte>
    {
        public int Value { get; private set; }
        public bool Wildcard
        {
            get { return wildcard; }
            set
            {
                if (wildcard != value)
                {
                    wildcard = value;
                    WildcardChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        private bool wildcard;
        public event EventHandler WildcardChanged;

        public CodeByte(int value)
        {
            Value = value;
            Wildcard = false;
        }

        public override bool Equals(object obj)
        {
            if (obj is CodeByte)
                return obj.GetHashCode() == GetHashCode();
            return false;
        }

        public bool Equals(CodeByte other)
        {
            return other != null &&
                   Value == other.Value &&
                   Wildcard == other.Wildcard;
        }

        public override int GetHashCode()
        {
            var hashCode = 1784914313;
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + Wildcard.GetHashCode();
            return hashCode;
        }
    }
}
