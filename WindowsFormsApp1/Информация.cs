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
    public partial class Информация : Form
    {
        EDWDCLASS edwd = new EDWDCLASS();
        public Информация()
        {
            InitializeComponent();
            timer1.Enabled = true;
            Properties.Settings s1 = new Properties.Settings();
            label9.Text = s1.Version;
            if(s1.Theme == "Dark")
            {
                this.BackColor = Color.Black;
                label1.BackColor = Color.Black;
                label1.ForeColor = Color.White;
                label2.BackColor = Color.Black;
                label2.ForeColor = Color.White;
                label3.BackColor = Color.Black;
                label3.ForeColor = Color.White;
                label4.BackColor = Color.Black;
                label4.ForeColor = Color.White;
                label5.BackColor = Color.Black;
                label5.ForeColor = Color.White;
                label6.BackColor = Color.Black;
                label6.ForeColor = Color.White;
                label7.BackColor = Color.Black;
                label7.ForeColor = Color.White;
                label8.BackColor = Color.Black;
                label8.ForeColor = Color.White;
                label9.BackColor = Color.Black;
                label9.ForeColor = Color.White;
                label10.BackColor = Color.Black;
                label10.ForeColor = Color.White;
                label11.BackColor = Color.Black;
                label11.ForeColor = Color.White;
                label12.BackColor = Color.Black;
                label12.ForeColor = Color.White;
                label13.BackColor = Color.Black;
                label13.ForeColor = Color.White;
                label14.BackColor = Color.Black;
                label14.ForeColor = Color.White;
                label15.BackColor = Color.Black;
                label15.ForeColor = Color.White;
                linkLabel1.BackColor = Color.Black;
                linkLabel2.BackColor = Color.Black;
            }
            else
            {
                this.BackColor = Color.White;
                label1.BackColor = Color.White;
                label1.ForeColor = Color.Black;
                label2.BackColor = Color.White;
                label2.ForeColor = Color.Black;
                label3.BackColor = Color.White;
                label3.ForeColor = Color.Black;
                label4.BackColor = Color.White;
                label4.ForeColor = Color.Black;
                label5.BackColor = Color.White;
                label5.ForeColor = Color.Black;
                label6.BackColor = Color.White;
                label6.ForeColor = Color.Black;
                label7.BackColor = Color.White;
                label7.ForeColor = Color.Black;
                label8.BackColor = Color.White;
                label8.ForeColor = Color.Black;
                label9.BackColor = Color.White;
                label9.ForeColor = Color.Black;
                label10.BackColor = Color.White;
                label10.ForeColor = Color.Black;
                label11.BackColor = Color.White;
                label11.ForeColor = Color.Black;
                label12.BackColor = Color.White;
                label12.ForeColor = Color.Black;
                label13.BackColor = Color.White;
                label13.ForeColor = Color.Black;
                label14.BackColor = Color.White;
                label14.ForeColor = Color.Black;
                label15.BackColor = Color.White;
                label15.ForeColor = Color.Black;
                linkLabel1.BackColor = Color.White;
                linkLabel2.BackColor = Color.White;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/bynare");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/bynare");
        }
        int kek = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if(kek == 0)
            {
                pictureBox1.Visible = true;
                pictureBox2.Visible = false;
                kek = 1;
            }
            else if(kek == 1)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
                kek = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            edwd.rebuildWD();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            edwd.rebuildsystem();
        }
    }
}
