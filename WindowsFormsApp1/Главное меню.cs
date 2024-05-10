using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Principal;
using System.Runtime.ConstrainedExecution;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private EDWDCLASS edwd = new EDWDCLASS();
        private Properties.Settings s1 = new Properties.Settings();
        public Form1()
        {
            InitializeComponent();
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            bool isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            if (isElevated == false)
            {
                MessageBox.Show("Вы запустили программу без прав администратора!\nПерезапустите программу что бы она работала корректно", "Non-Admin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                Environment.Exit(0);
            }
            else
            {
                if (Environment.OSVersion.Version.Major < 6)
                {
                    MessageBox.Show("Ваша версия windows не поддерживается приложением. Sorry!", "Ooops...", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    Environment.Exit(0);
                }
                else MessageBox.Show("Данная программа может .N4S1LN0. выключать windows defender\nС последними обновлениями функционал намного больше расширился", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            
            if (s1.Logs == 1)
            {
                Width = 729;
                Height = 375;
                textBox1.Enabled = true;
                textBox1.Visible = true;
            }
            else if (s1.Logs == 0)
            {
                Width = 729;
                Height = 250;
                textBox1.Enabled = false;
                textBox1.Visible = false;
            }
            if(s1.Theme == "Dark")
            {
                BackColor = Color.Black;
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                настройкиToolStripMenuItem1.ForeColor = Color.White;
                оПрограммеToolStripMenuItem1.ForeColor = Color.White;
                оПрограммеToolStripMenuItem1.BackColor = Color.Black;
                экспериментальныеНастройкиToolStripMenuItem.ForeColor = Color.White;
                menuStrip1.BackColor = Color.Black;
                textBox1.ForeColor = Color.White;
                textBox1.BackColor = Color.Black;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
            }
            else
            {
                BackColor = Color.White;
                label1.ForeColor = Color.Black;
                label2.ForeColor = Color.Black;
                настройкиToolStripMenuItem1.ForeColor = Color.Black;
                оПрограммеToolStripMenuItem1.ForeColor = Color.Black;
                оПрограммеToolStripMenuItem1.BackColor = Color.White;
                экспериментальныеНастройкиToolStripMenuItem.ForeColor = Color.Black;
                menuStrip1.BackColor = Color.White;
                textBox1.ForeColor = Color.Black;
                textBox1.BackColor = Color.White;
                label3.ForeColor = Color.Black;
                label4.ForeColor = Color.Black;
            }
            checkzashitnik("defender");
            checkzashitnik("firewall");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)) return;
            DialogResult result = MessageBox.Show("Прежде чем полностью выключить дефендер через программу, убедитесь что вы самостоятельно выключили. Вы ведь его выключили?", "INFO", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                progressBar1.Value = 0;
                textBox1.Text = "";
                edwd.zapol(50);
                edwd.disabledefender();
            }
            else
            {
                MessageBox.Show("Тогда выключите полностью в настройках антивирусника все пункты, и потом запустите процесс выключения defender'a еще раз\nГде его выключить: Защита от вирусов и угроз -> Управление настройками", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(@"C:\Program Files\WindowsApps\Microsoft.SecHealthUI_1000.25873.9001.0_x64__8wekyb3d8bbwe\SecHealthUI.exe");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator)) return;
            progressBar1.Value = 0;
            textBox1.Text = "";
            edwd.zapol(50);
            edwd.enabledefender();
        }
        internal void checkzashitnik(string vibor)
        {
            if(vibor == "defender")
            {
                //string TamperProtection = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "TamperProtection", null)?.ToString();
                string DisableAntiSpyware = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", null)?.ToString();
                //string DisableBehaviorMonitoring = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring", null)?.ToString();
                //string DisableOnAccessProtection = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection", null)?.ToString();
                //string DisableScanOnRealtimeEnable = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable", null)?.ToString();
                if (/*TamperProtection == "0" && */DisableAntiSpyware == "1" /*&& DisableBehaviorMonitoring == "1" && DisableOnAccessProtection == "1" && DisableScanOnRealtimeEnable == "1"*/)
                {
                    label2.Text = "Отключен";
                    pictureBox2.Visible = false;
                    pictureBox1.Visible = true;
                }
                else
                {
                    label2.Text = "Включен";
                    pictureBox1.Visible = false;
                    pictureBox2.Visible = true;
                }
            }
            else if (vibor == "firewall")
            {
                string FirewallProtection = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SharedAccess\Parameters\FirewallPolicy\StandardProfile", "EnableFirewall", null)?.ToString();
                if (FirewallProtection == "1")
                {
                    label3.Text = "Включен";
                    pictureBox4.Visible = false;
                    pictureBox3.Visible = true;
                }
                else if (FirewallProtection == "0")
                {
                    label3.Text = "Отключен";
                    pictureBox4.Visible = true;
                    pictureBox3.Visible = false;
                }
            }
        }

        private void настройкиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            averagesettings1 settings = new averagesettings1();
            settings.Show();
        }

        private void оПрограммеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Информация svedenie = new Информация();
            svedenie.Show();
        }

        private void экспериментальныеНастройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("ВНИМАНИЕ!!!!\nРазработчик не несет ответственность за дальнейшие действия(в том числе за действия с вашей ОС и вашим ПК)\nОсознанность данных действий остается только за вами", "WARNING", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                MessageBox.Show("Выбор остается за вами. Вы выбрали рисковый путь.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                s1.extremalSET = 1;
                s1.Save();
                экспериментальные_настройки noaverageusersetting = new экспериментальные_настройки();
                noaverageusersetting.Show();
            }
            else if (result == DialogResult.Cancel)
            {
                MessageBox.Show("Выбор остается за вами. Вы выбрали благородный путь", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                s1.extremalSET = 0;
                s1.Save();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            edwd.enablefirewall();
            label3.Text = "Включен";
            pictureBox4.Visible = false;
            pictureBox3.Visible = true;
          
        }

        private void button10_Click(object sender, EventArgs e)
        {
            edwd.disablefirewall();
            label3.Text = "Отключен";
            pictureBox4.Visible = true;
            pictureBox3.Visible = false;
        }
    }
}