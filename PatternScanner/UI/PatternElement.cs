using System;
using System.Drawing;
using System.Windows.Forms;
using PatternScanner.DTO;

namespace PatternScanner.UI
{
    public partial class PatternElement : UserControl
    {
        public CodeByte CodeByte
        {
            get { return codeByte; }
            set
            {
                if (codeByte != value)
                {
                    codeByte = value;
                    Wildcard = codeByte.Wildcard;

                    if (codeByte != null)
                    {
                        lblWildcard.Text = codeByte.Wildcard ? "[?]" : "[x]";
                        this.BackColor = codeByte.Wildcard ? Color.White : Color.Gray;
                        lblValue.Text = codeByte.Value.ToString("X2");
                    }
                }
            }
        }
        public bool Wildcard
        {
            get { return codeByte.Wildcard; }
            set { codeByte.Wildcard = value; }
        }
        private CodeByte codeByte;

        public event EventHandler WildcardChanged;

        public PatternElement(CodeByte codeByte)
        {
            InitializeComponent();
            CodeByte = codeByte;
            CodeByte.WildcardChanged += WildcardChangedEvent;
        }

        private void WildcardChangedEvent(object sender, EventArgs e)
        {
            lblWildcard.Text = codeByte.Wildcard ? "[?]" : "[x]";
            this.BackColor = codeByte.Wildcard ? Color.White : Color.Gray;
            WildcardChanged.Invoke(this, EventArgs.Empty);
        }

        private void lblWildcard_Click(object sender, EventArgs e)
        {
            Wildcard = !Wildcard;
        }

        private void lblValue_Click(object sender, EventArgs e)
        {
            Wildcard = !Wildcard;
        }

        private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            Wildcard = !Wildcard;
        }
    }
}
