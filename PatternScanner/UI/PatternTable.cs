using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PatternScanner.DTO;

namespace PatternScanner.UI
{
    public partial class PatternTable : UserControl
    {
        public CodeText CodeText
        {
            get { return codeText; }
            set
            {
                if (codeText != value)
                {
                    codeText = value;
                    table.SuspendLayout();
                    ClearRows();
                    if (codeText != null)
                        AddRows();
                    table.ResumeLayout();
                }
            }
        }
        private CodeText codeText;
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
            table.RowCount = codeText.Rows.Length;

            for (int i = 0; i < CodeText.Rows.Length; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                var patternRow = new PatternRow(codeText.Rows[i])
                {
                    Anchor = AnchorStyles.Left
                };
                var textLabel = new Label()
                {
                    Anchor = AnchorStyles.Left,
                    AutoSize = true,
                    Font = CodeFont,
                    Text = codeText.Rows[i].Text
                };

                patternRow.PatternChanged += (o, e) => PatternChanged?.Invoke(this, EventArgs.Empty);
                
                table.Controls.Add(patternRow, 0, i);
                table.Controls.Add(textLabel, 1, i);
            }
        }
    }
}
