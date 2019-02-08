using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using PatternScanner.DTO;

namespace PatternScanner.UI
{
    public partial class PatternRow : UserControl
    {
        public CodeRow CodeRow { get; private set; }
        public PatternElement[] Elements
        {
            get { return panel.Controls.Cast<PatternElement>().ToArray(); }
            set
            {
                var elements = panel.Controls.Cast<PatternElement>().ToArray();
                var add = value.Where(x => !elements.Contains(x)).ToArray();
                var remove = elements.Where(x => !value.Contains(x)).ToArray();
                panel.SuspendLayout();
                if (add.Length > 0)
                {
                    panel.Controls.AddRange(add);
                    foreach (var c in add)
                        c.WildcardChanged += WildCardChangedEvent;
                }
                if (remove.Length > 0)
                {
                    foreach (var c in remove)
                    {
                        panel.Controls.Remove(c);
                        c.WildcardChanged -= WildCardChangedEvent;
                    }
                }
                panel.ResumeLayout();
            }
        }

        public event EventHandler PatternChanged;

        private void WildCardChangedEvent(object sender, EventArgs e)
        {
            PatternChanged?.Invoke(this, EventArgs.Empty);
        }

        public PatternRow(CodeRow codeRow)
        {
            InitializeComponent();
            CodeRow = codeRow;
            Elements = CodeRow.Bytes.Select(x => new PatternElement(x)).ToArray();
        }
    }
}
