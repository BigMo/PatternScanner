using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PatternScanner.DTO;
using PatternScanner.DTO.Code;

namespace PatternScanner.UI
{
    public partial class PatternTable : UserControl
    {
        public CodeText CodeText
        {
            get { return pattern?.CodeText ?? null; }
            set
            {
                if (pattern != null)
                {
                    pattern.CodeText = value;
                    table.SuspendLayout();
                    ClearRows();
                    if (pattern.CodeText != null)
                        AddRows();
                    table.ResumeLayout();
                    PatternChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public Pattern Pattern
        {
            get { return pattern; }
            set
            {
                if (pattern != value)
                {
                    pattern = value;
                    CodeText = pattern.CodeText;
                }
            }
        }

        //private CodeText codeText;
        private Pattern pattern;
        //private CodeText codeText;
        public event EventHandler PatternChanged;
        public Font CodeFont { get; set; }

        public PatternTable()
        {
            InitializeComponent();
            table.RowCount = 0;
            CodeFont = Font;
        }

        private void ClearRows()
        {
            table.Controls.Clear();
            table.RowStyles.Clear();
            table.RowCount = 0;
        }
        private void AddRows()
        {
            table.RowCount = pattern.CodeText.Rows.Length;

            for (int i = 0; i < CodeText.Rows.Length; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                var patternRow = new PatternRow(pattern.CodeText.Rows[i])
                {
                    Anchor = AnchorStyles.Left
                };
                var textLabel = new Label()
                {
                    Anchor = AnchorStyles.Left,
                    AutoSize = true,
                    Font = CodeFont,
                    Text = pattern.CodeText.Rows[i].Text
                };

                patternRow.PatternChanged += (o, e) =>
                {
                    pattern.ApplyMask(CodeText.Mask);
                    PatternChanged?.Invoke(this, EventArgs.Empty);
                };
                

                table.Controls.Add(patternRow, 0, i);
                table.Controls.Add(textLabel, 1, i);
            }
        }
    }
}
