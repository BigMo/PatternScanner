using PatternScanner.DTO;
using PatternScanner.DTO.Code;
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
        //public CodeText CodeText
        //{
        //    get { return codeText; }
        //    set
        //    {
        //        if (codeText != value)
        //        {
        //            codeText = value;
        //            btnApply.Enabled = codeText != null && CodeText.Rows.Length > 0 && codeText.Rows.SelectMany(x => x.Bytes).Count() > 0;
        //            lblRows.Text = codeText == null ? "-" : codeText.Rows.Length.ToString();
        //            lblBytes.Text = codeText == null ? "-" : codeText.Rows.SelectMany(x => x.Bytes).Count().ToString();
        //            rtbText.Text = codeText == null ? "" : codeText.Source;
        //            txbName.Text = codeText == null ? "" : codeText.Name;
        //        }
        //    }
        //}

        //private CodeText codeText;

        public Pattern Pattern
        {
            get { return pattern; }
            set
            {
                if (pattern != value)
                {
                    pattern = value;
                    cbbStyle.SelectedItem = pattern.Parser;
                    btnApply.Enabled = pattern != null;
                    lblRows.Text = pattern == null ? "-" : pattern.CodeText.Rows.Length.ToString();
                    lblBytes.Text = pattern == null ? "-" : pattern.Bytes.Length.ToString();
                    rtbText.Text = pattern == null ? "" : pattern.Source;
                    txbName.Text = pattern == null ? "" : pattern.Name;
                }
            }
        }
        private Pattern pattern;
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
            //try
            //{
                Pattern = Pattern.FromParser(rtbText.Text, txbName.Text, Parser);
                txbName.Text = Pattern.Name;
                //CodeText = Parser.Parse(rtbText.Text);
                //txbName.Text = CodeText.Pattern.Name;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"Failed to parse disassembly:\n{ex.Message}", "Error", MessageBoxButtons.OK);
            //    //CodeText = null;
            //}
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
            catch (Exception ex)
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
            //if (codeText == null || (codeText != null && rtbText.Text != codeText.Source))
            //    btnApply.Enabled = false;
            if (Pattern == null || (Pattern != null && rtbText.Text != Pattern.Source))
                btnApply.Enabled = false;
        }

        private void txbName_TextChanged(object sender, EventArgs e)
        {
            if (Pattern != null)
                Pattern.Name = txbName.Text;
            //if (codeText != null)
            //    codeText.Name = txbName.Text;
        }
    }
}
