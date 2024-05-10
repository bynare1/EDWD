using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class экспериментальные_настройки : Form
    {
        private Properties.Settings s1 = new Properties.Settings();
        public экспериментальные_настройки()
        {
            InitializeComponent();
        }
        private EDWDCLASS edwd = new EDWDCLASS();
        private void button4_Click(object sender, EventArgs e)
        {
            edwd.deletehelp();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            edwd.deletephoneconnection();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            edwd.deletexboxgamebar();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            edwd.deletecamera();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            edwd.deleteonedrive();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            edwd.deletecortana();
        }

        private void экспериментальные_настройки_Load(object sender, EventArgs e)
        {
            if (s1.Theme == "Dark") BackColor = Color.Black;
            else if (s1.Theme == "White") BackColor = Color.White;
        }
    }
}
