using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Security.Principal;
using System.Drawing;

namespace WindowsFormsApp1
{
    class EDWDCLASS
    {
        string govor = " ставим в: ";
        Properties.Settings s1 = new Properties.Settings();

        internal void rebuildWD()
        {
            RunPS("regsvr32 atl.dll");
            RunPS("regsvr32 wuapi.dll");
            RunPS("regsvr32 softpub.dll");
            RunPS("regsvr32 mssip32.dll");
            if(Environment.Is64BitOperatingSystem) RunCMD("/K %SystemRoot%\\sysNative\\Dism.exe /Online /Cleanup-Image /RestoreHealth", ProcessWindowStyle.Normal);//%SystemRoot%\sysNative\Dism.exe /online blablabla
            else RunCMD("/K DISM /Online /Cleanup-Image /RestoreHealth", ProcessWindowStyle.Normal);
        }
        internal void disablefirewall()
        {
            RunCMD("/C netsh advfirewall set allprofiles state off", ProcessWindowStyle.Hidden);
            //Form1 f1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            //f1.checkzashitnik("firewall");
        }
        internal void enablefirewall()
        {
            RunCMD("/C netsh advfirewall set allprofiles state on", ProcessWindowStyle.Hidden);
            //Form1 f1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            //f1.checkzashitnik("firewall");
        }
        internal void rebuildsystem()
        {
            RunCMD("/K C:\\Windows\\sysNative\\sfc.exe /scannow", ProcessWindowStyle.Normal);
        }

        internal void deleteonedrive()
        {
            //RunPS("Stop-Process -Name OneDrive -Force -ErrorAction 0");
            RunCMD("/K C:\\Windows\\sysNative\\OneDriveSetup.exe /uninstall", ProcessWindowStyle.Normal);
        }
        
        internal void deletecortana()
        {
            RunPS("Get-AppxPackage -allusers *Microsoft.549981C3F5F10* | Remove-AppxPackage");
            MessageBox.Show("Cortana удалена", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        internal void deletehelp()
        {
            RunPS("Get-AppxPackage -allusers *Microsoft.GetHelp* | Remove-AppxPackage");
            MessageBox.Show("Microsoft Поддержка удалена", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        internal void deletephoneconnection()
        {
            RunPS("Get-AppxPackage -allusers *Microsoft.YourPhone* | Remove-AppxPackage");
            MessageBox.Show("Связь с телефоном удалена", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        internal void deletexboxgamebar()
        {
            RunPS("Get-AppxPackage -allusers *Microsoft.XboxGamingOverlay* | Remove-AppxPackage");
            RunPS("Get-AppxPackage -allusers *Microsoft.XboxGameOverlay* | Remove-AppxPackage");
            RunPS("Get-AppxPackage -allusers *Microsoft.XboxSpeechToTextOverlay* | Remove-AppxPackage");
            //Microsoft.XboxGameOverlay Microsoft.XboxSpeechToTextOverlay
            MessageBox.Show("Xbox Game Bar удален", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        internal void deletecamera()
        {
            RunPS("Get-AppxPackage -allusers *Microsoft.WindowsCamera* | Remove-AppxPackage");
            MessageBox.Show("Камера удалена", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        internal void disabledefender()
        {
            Form1 f1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            RegistryEdit(@"SOFTWARE\Microsoft\Windows Defender\Features", "TamperProtection", "0"); //Windows 10 1903 Redstone 6
            if (s1.Logs == 1) f1.textBox1.Text += "TamperProtection" + govor + "0" + Environment.NewLine;
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "1");
            if (s1.Logs == 1) f1.textBox1.Text += "DisableAntiSpyware" + govor + "1" + Environment.NewLine;
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring", "1");
            if (s1.Logs == 1) f1.textBox1.Text += "DisableBehaviorMonitoring" + govor + "1" + Environment.NewLine;
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection", "1");
            if (s1.Logs == 1) f1.textBox1.Text += "DisableOnAccessProtection" + govor + "1" + Environment.NewLine;
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable", "1");
            if (s1.Logs == 1) f1.textBox1.Text += "DisableScanOnRealtimeEnable" + govor + "1" + Environment.NewLine;


            //Компьютер\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\PolicyManager\default\Defender\AllowScanningNetworkFiles
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Scan", "DisableArchiveScanning", "0");//0-disable;1-enable
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Scan", "DisableEmailScanning", "0");//0-disable;1-enable                                                                                             //DisableEmailScanning
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Scan", "DisableScanningMappedNetworkDrivesForFullScan", "0");//0-disable;1-enable
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Scan", "DisableRemovableDriveScanning", "0");//0-disable;1-enable
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Scan", "DisableScanningNetworkFiles", "0");//0-disable;1-enable
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = "Get-MpPreference -verbose",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                //Form1 f1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
                if (line.StartsWith(@"DisableRealtimeMonitoring") && line.EndsWith("False"))
                {
                    RunPS("Set-MpPreference -DisableRealtimeMonitoring $true"); //real-time protection
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableRealtimeMonitoring" + govor + "true" + Environment.NewLine;
                }

                else if (line.StartsWith(@"DisableBehaviorMonitoring") && line.EndsWith("False"))
                {
                    RunPS("Set-MpPreference -DisableBehaviorMonitoring $true"); //behavior monitoring
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableBehaviorMonitoring" + govor + "true" + Environment.NewLine;
                }

                else if (line.StartsWith(@"DisableBlockAtFirstSeen") && line.EndsWith("False"))
                {
                    RunPS("Set-MpPreference -DisableBlockAtFirstSeen $true");
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableBlockAtFirstSeen" + govor + "true" + Environment.NewLine;
                }

                else if (line.StartsWith(@"DisableIOAVProtection") && line.EndsWith("False"))
                {
                    RunPS("Set-MpPreference -DisableIOAVProtection $true"); //scans all downloaded files and attachments
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableIOAVProtection" + govor + "true" + Environment.NewLine;
                }

                else if (line.StartsWith(@"DisablePrivacyMode") && line.EndsWith("False"))
                {
                    RunPS("Set-MpPreference -DisablePrivacyMode $true"); //displaying threat history
                    if (s1.Logs == 1) f1.textBox1.Text += "DisablePrivacyMode" + govor + "true" + Environment.NewLine;
                }   

                else if (line.StartsWith(@"SignatureDisableUpdateOnStartupWithoutEngine") && line.EndsWith("False"))
                {
                    RunPS("Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $true"); //definition updates on startup
                    if (s1.Logs == 1) f1.textBox1.Text += "SignatureDisableUpdateOnStartupWithoutEngine" + govor + "true" + Environment.NewLine;
                }

                else if (line.StartsWith(@"DisableArchiveScanning") && line.EndsWith("False"))
                {
                    RunPS("Set-MpPreference -DisableArchiveScanning $true"); //scan archive files, such as .zip and .cab files
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableArchiveScanning" + govor + "true" + Environment.NewLine;
                }

                else if (line.StartsWith(@"DisableIntrusionPreventionSystem") && line.EndsWith("False"))
                {
                    RunPS("Set-MpPreference -DisableIntrusionPreventionSystem $true"); // network protection
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableIntrusionPreventionSystem" + govor + "true" + Environment.NewLine;
                } 

                else if (line.StartsWith(@"DisableScriptScanning") && line.EndsWith("False"))
                {
                    RunPS("Set-MpPreference -DisableScriptScanning $true"); //scanning of scripts during scans
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableScriptScanning" + govor + "true" + Environment.NewLine;
                }

                else if (line.StartsWith(@"SubmitSamplesConsent") && !line.EndsWith("2"))
                {
                    RunPS("Set-MpPreference -SubmitSamplesConsent 2"); //MAPSReporting
                    if (s1.Logs == 1) f1.textBox1.Text += "SubmitSamplesConsent" + govor + "2" + Environment.NewLine;
                }

                else if (line.StartsWith(@"MAPSReporting") && !line.EndsWith("0"))
                {
                    RunPS("Set-MpPreference -MAPSReporting 0"); //MAPSReporting
                    if (s1.Logs == 1) f1.textBox1.Text += "MAPSReporting" + govor + "0" + Environment.NewLine;
                }

                else if (line.StartsWith(@"HighThreatDefaultAction") && !line.EndsWith("6"))
                {
                    RunPS("Set-MpPreference -HighThreatDefaultAction 6 -Force"); // high level threat // Allow
                    if (s1.Logs == 1) f1.textBox1.Text += "HighThreatDefaultAction" + govor + "6" + " с параметром: -Force" + Environment.NewLine;
                }

                else if (line.StartsWith(@"ModerateThreatDefaultAction") && !line.EndsWith("6"))
                {
                    RunPS("Set-MpPreference -ModerateThreatDefaultAction 6"); // moderate level threat
                    if (s1.Logs == 1) f1.textBox1.Text += "ModerateThreatDefaultAction" + govor + "6" + Environment.NewLine;
                }

                else if (line.StartsWith(@"LowThreatDefaultAction") && !line.EndsWith("6"))
                {
                    RunPS("Set-MpPreference -LowThreatDefaultAction 6"); // low level threat
                    if (s1.Logs == 1) f1.textBox1.Text += "LowThreatDefaultAction" + govor + "6" + Environment.NewLine;
                }

                else if (line.StartsWith(@"SevereThreatDefaultAction") && !line.EndsWith("6"))
                {
                    RunPS("Set-MpPreference -SevereThreatDefaultAction 6"); // severe level threat
                    if (s1.Logs == 1) f1.textBox1.Text += "SevereThreatDefaultAction" + govor + "6" + Environment.NewLine;
                }
            }
            zapol(50);
            //Form1 f1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            f1.checkzashitnik("defender");
            MessageBox.Show("Windows Defender выключен", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        internal void enabledefender()
        {
            Form1 f1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            RegistryEdit(@"SOFTWARE\Microsoft\Windows Defender\Features", "TamperProtection", "1"); //Windows 10 1903 Redstone 6
            if (s1.Logs == 1) f1.textBox1.Text += "TamperProtection" + govor + "1" + Environment.NewLine;
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender", "DisableAntiSpyware", "0");
            if (s1.Logs == 1) f1.textBox1.Text += "DisableAntiSpyware" + govor + "0" + Environment.NewLine;
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableBehaviorMonitoring", "0");
            if (s1.Logs == 1) f1.textBox1.Text += "DisableBehaviorMonitoring" + govor + "0" + Environment.NewLine;
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableOnAccessProtection", "0");
            if (s1.Logs == 1) f1.textBox1.Text += "DisableOnAccessProtection" + govor + "0" + Environment.NewLine;
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection", "DisableScanOnRealtimeEnable", "0");
            if (s1.Logs == 1) f1.textBox1.Text += "DisableScanOnRealtimeEnable" + govor + "0" + Environment.NewLine;

            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Scan", "DisableArchiveScanning", "1");//0-disable;1-enable
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Scan", "DisableEmailScanning", "1");//0-disable;1-enable                                                                                             //DisableEmailScanning
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Scan", "DisableScanningMappedNetworkDrivesForFullScan", "1");//0-disable;1-enable
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Scan", "DisableRemovableDriveScanning", "1");//0-disable;1-enable
            RegistryEdit(@"SOFTWARE\Policies\Microsoft\Windows Defender\Scan", "DisableScanningNetworkFiles", "1");//0-disable;1-enable
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = "Get-MpPreference -verbose",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                if (line.StartsWith(@"DisableRealtimeMonitoring") && line.EndsWith("true"))
                {
                    RunPS("Set-MpPreference -DisableRealtimeMonitoring $False"); //real-time protection
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableRealtimeMonitoring" + govor + "False" + Environment.NewLine;
                }
                    
                else if (line.StartsWith(@"DisableBehaviorMonitoring") && line.EndsWith("true"))
                {
                    RunPS("Set-MpPreference -DisableBehaviorMonitoring $False"); //behavior monitoring
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableBehaviorMonitoring" + govor + "False" + Environment.NewLine;
                }
                    
                else if (line.StartsWith(@"DisableBlockAtFirstSeen") && line.EndsWith("true"))
                {
                    RunPS("Set-MpPreference -DisableBlockAtFirstSeen $False");
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableBlockAtFirstSeen" + govor + "False" + Environment.NewLine;
                }

                else if (line.StartsWith(@"DisableIOAVProtection") && line.EndsWith("true"))
                {
                    RunPS("Set-MpPreference -DisableIOAVProtection $False"); //scans all downloaded files and attachments
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableIOAVProtection" + govor + "False" + Environment.NewLine;
                }

                else if (line.StartsWith(@"DisablePrivacyMode") && line.EndsWith("true"))
                {
                    RunPS("Set-MpPreference -DisablePrivacyMode $False"); //displaying threat history
                    if (s1.Logs == 1) f1.textBox1.Text += "DisablePrivacyMode" + govor + "False" + Environment.NewLine;
                }

                else if (line.StartsWith(@"SignatureDisableUpdateOnStartupWithoutEngine") && line.EndsWith("true"))
                {
                    RunPS("Set-MpPreference -SignatureDisableUpdateOnStartupWithoutEngine $False"); //definition updates on startup
                    if (s1.Logs == 1) f1.textBox1.Text += "SignatureDisableUpdateOnStartupWithoutEngine" + govor + "False" + Environment.NewLine;
                }

                else if (line.StartsWith(@"DisableArchiveScanning") && line.EndsWith("true"))
                {
                    RunPS("Set-MpPreference -DisableArchiveScanning $False"); //scan archive files, such as .zip and .cab files
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableArchiveScanning" + govor + "False" + Environment.NewLine;
                }

                else if (line.StartsWith(@"DisableIntrusionPreventionSystem") && line.EndsWith("true"))
                {
                    RunPS("Set-MpPreference -DisableIntrusionPreventionSystem $False"); // network protection
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableIntrusionPreventionSystem" + govor + "False" + Environment.NewLine;
                }

                else if (line.StartsWith(@"DisableScriptScanning") && line.EndsWith("true"))
                {
                    RunPS("Set-MpPreference -DisableScriptScanning $False"); //scanning of scripts during scans
                    if (s1.Logs == 1) f1.textBox1.Text += "DisableScriptScanning" + govor + "False" + Environment.NewLine;
                }

                else if (line.StartsWith(@"SubmitSamplesConsent") && line.EndsWith("2"))
                {
                    RunPS("Set-MpPreference -SubmitSamplesConsent 1"); //SubmitSamplesConsent 0-спрашивать, 1-безопасно отправлять(по умолчанию) , 2-никогда,3-все автоматом отправлять
                    if (s1.Logs == 1) f1.textBox1.Text += "SubmitSamplesConsent" + govor + "1" + Environment.NewLine;
                }

                else if (line.StartsWith(@"MAPSReporting") && line.EndsWith("0"))
                {
                    RunPS("Set-MpPreference -MAPSReporting 1"); /*MAPSReporting 
                    * 0: Disabled
                    * 1: Базовое членство.
                    * 2: Расширенное членство*/
                    if (s1.Logs == 1) f1.textBox1.Text += "MAPSReporting" + govor + "1" + Environment.NewLine;
                }

                else if (line.StartsWith(@"HighThreatDefaultAction") && line.EndsWith("6"))
                {
                    RunPS("Set-MpPreference -HighThreatDefaultAction 3"); // high level threat:(1-7) Clean, Quarantine, Remove, Allow, UserDefined, NoAction, Block
                    if (s1.Logs == 1) f1.textBox1.Text += "HighThreatDefaultAction" + govor + "3" + Environment.NewLine;
                }
                    

                else if (line.StartsWith(@"ModerateThreatDefaultAction") && line.EndsWith("6"))
                {
                    RunPS("Set-MpPreference -ModerateThreatDefaultAction 3"); // moderate level threat:(1-7) Clean, Quarantine, Remove, Allow, UserDefined, NoAction, Block
                    if (s1.Logs == 1) f1.textBox1.Text += "ModerateThreatDefaultAction" + govor + "3" + Environment.NewLine;
                }

                else if (line.StartsWith(@"LowThreatDefaultAction") && line.EndsWith("6"))
                {
                    RunPS("Set-MpPreference -LowThreatDefaultAction 3"); // low level threat:(1-7) Clean, Quarantine, Remove, Allow, UserDefined, NoAction, Block
                    if (s1.Logs == 1) f1.textBox1.Text += "LowThreatDefaultAction" + govor + "3" + Environment.NewLine;
                }

                else if (line.StartsWith(@"SevereThreatDefaultAction") && line.EndsWith("6"))
                {
                    RunPS("Set-MpPreference -SevereThreatDefaultAction 3"); // severe level threat:(1-7) Clean, Quarantine, Remove, Allow, UserDefined, NoAction, Block  
                    if (s1.Logs == 1) f1.textBox1.Text += "SevereThreatDefaultAction" + govor + "3" + Environment.NewLine;
                }
            }
            zapol(50);
            //Form1 f1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            f1.checkzashitnik("defender");
            MessageBox.Show("Windows Defender включен", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private static void RegistryEdit(string regPath, string name, string value)
        {
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (key == null)
                    {
                        Registry.LocalMachine.CreateSubKey(regPath).SetValue(name, value, RegistryValueKind.DWord);
                        return;
                    }
                    if (key.GetValue(name) != (object)value) key.SetValue(name, value, RegistryValueKind.DWord);
                }
            }
            catch { }
        }
        private static void RunPS(string args)
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = args,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    CreateNoWindow = true
                }
            };
            proc.Start();
        }
        internal void RunCMD(string args, ProcessWindowStyle Style)
        {
            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = $@"{ args }",
                    UseShellExecute = true,
                    WindowStyle = Style,
                    Verb = "runas",
                    CreateNoWindow = true
                }
            };
            proc.Start();
        }
        internal void zapol(int i)
        {
            Form1 f1 = Application.OpenForms.OfType<Form1>().FirstOrDefault();
            for (int j = 0; j < i; j++) f1.progressBar1.Value++;
        }
    }
}
