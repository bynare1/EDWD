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
    public partial class averagesettings1 : Form
    {
        private Properties.Settings s1 = new Properties.Settings();
        public averagesettings1()
        {
            InitializeComponent();
            if (s1.Logs == 1) checkBox2.Checked = true;
            else checkBox2.Checked = false;
            if (s1.Theme == "Dark")
            {
                checkBox3.Checked = true;
                darkthemeform3();
            }
            else
            {
                checkBox3.Checked = false;
                whitethemeform3();
            }
        }
        private void whitethemeform3()
        {
            this.BackColor = Color.White;
            checkBox2.BackColor = Color.White;
            checkBox2.ForeColor = Color.Black;
            checkBox3.BackColor = Color.White;
            checkBox3.ForeColor = Color.Black;
        }
        private void darkthemeform3()
        {
            this.BackColor = Color.Black;
            checkBox2.BackColor = Color.Black;
            checkBox2.ForeColor = Color.White;
            checkBox3.BackColor = Color.Black;
            checkBox3.ForeColor = Color.White;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Form1 f1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            if (checkBox2.Checked == true)
            {
                s1.Logs = 1;
                f1.Width = 747;
                f1.Height = 375;
                f1.textBox1.Enabled = true;
                f1.textBox1.Visible = true;
                s1.Save();
            }
            else if (checkBox2.Checked == false)
            {
                s1.Logs = 0;
                f1.textBox1.Enabled = false;
                f1.textBox1.Visible = false;
                f1.Width = 747;
                f1.Height = 250;
                s1.Save();
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            //Form1 f1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            if (checkBox3.Checked == true)
            {
                s1.Theme = "Dark";
                //darkthemeform3();
                s1.Save();
                if (this.BackColor != Color.Black && this.Visible)
                {
                    darkthemeform3();
                    Application.Restart();
                }
            }
            else if (checkBox3.Checked == false)
            {
                s1.Theme = "Light";
                s1.Save();
                //edwd.lighttheme();
                if (this.BackColor != Color.White)
                {
                    whitethemeform3();
                    Application.Restart();
                }
            }
        }
    }
}
