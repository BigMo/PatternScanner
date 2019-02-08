using PatternScanner.DTO;
using PatternScanner.Parsing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatternScanner.UI
{
    public partial class frmPatternImport : Form
    {
        public ICodeParser Parser
        {
            get { return parser; }
            set
            {
                if (parser != value)
                {
                    parser = value;
                    btnParse.Enabled = parser != null;
                }
            }
        }
        public CodeText CodeText
        {
            get { return codeText; }
            set
            {
                if (codeText != value)
                {
                    codeText = value;
                    btnApply.Enabled = codeText != null && CodeText.Rows.Length > 0 && codeText.Rows.SelectMany(x => x.Bytes).Count() > 0;
                    lblRows.Text = codeText == null ? "-" : codeText.Rows.Length.ToString();
                    lblBytes.Text = codeText == null ? "-" : codeText.Rows.SelectMany(x => x.Bytes).Count().ToString();
                }
            }
        }

        private CodeText codeText;
        private ICodeParser parser;


        public frmPatternImport()
        {
            InitializeComponent();
            Icon = Properties.Resources.Logo_256;
            cbbStyle.DataSource = Parsers.All;
            cbbStyle.SelectedItem = Parsers.All[0];
        }

        private void cbbStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Parser = (ICodeParser)cbbStyle.SelectedItem;
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            try
            {
                CodeText = Parser.Parse(rtbText.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to parse disassembly:\n{ex.Message}", "Error", MessageBoxButtons.OK);
                CodeText = null;
            }
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (var diag = new OpenFileDialog())
                {
                    diag.Title = "Select text file";
                    diag.Filter = "Text file (*.txt)|*.txt";
                    if (diag.ShowDialog() == DialogResult.OK)
                    {
                        rtbText.Text = File.ReadAllText(diag.FileName);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Failed to read file:\n{ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        private void btnClipboard_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                rtbText.Text = Clipboard.GetText();
            }
            else
            {
                MessageBox.Show("The clipboard is empty.", "Error", MessageBoxButtons.OK);
            }
        }

        private void rtbText_TextChanged(object sender, EventArgs e)
        {
            CodeText = null;
        }
    }
}
