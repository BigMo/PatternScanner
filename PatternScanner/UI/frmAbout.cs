using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatternScanner.UI
{
    public partial class frmAbout : Form
    {
        public frmAbout()
        {
            InitializeComponent();
            Icon = Properties.Resources.Logo_256;

            lblGithubZat.Click += (o, e) => Process.Start("https://github.com/BigMo");
            lblGithubScanner.Click += (o, e) => Process.Start("https://github.com/BigMo/PatternScanner");
            lblUCZat.Click += (o, e) => Process.Start("https://www.unknowncheats.me/forum/members/562321.html");
            //Not released yet
            //lblUCScanner.Click += (o, e) => Process.Start("");

            rtbGreetings.Text = RandomGreetings();
        }

        private string RandomGreetings()
        {
            var names = new string[] { "D34Dspy", "Sh8dow", "striek", "Atex", "xeno", "polivilas", "B74k", "Daax", "Janck7", "KN4CK3R", "patrik", "AestheticJunk", "Geertje123", "Puddin Poppin", "KingRain", "SeanGhost117", "sk0r", "Slayer", "Zooom" };
            var random = new byte[names.Length];
            RNGCryptoServiceProvider.Create().GetBytes(random);

            var list = new List<string>(names);
            var order = random.Select(x =>
            {
                var idx = x % list.Count;
                var name = list[idx];
                list.RemoveAt(idx);
                return name;
            });

            return string.Join(", ", order.ToArray());
        }
    }
}
