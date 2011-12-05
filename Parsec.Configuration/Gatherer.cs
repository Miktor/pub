using System;
using System.Text;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Security.AccessControl;
using System.Collections.Generic;

namespace Parsec.Configuration
{     

    public class ProgressChangedEventArgs : EventArgs
    {
        public ProgressChangedEventArgs(int value, string capture)
        {
            this.value = value;
            this.capture = capture;
        }

        public int value;
        public string capture;
    }

    class Gatherer
    {
        [DllImport("Advapi32.dll")]
        public static extern bool BackupEventLog(IntPtr hd, string backupFileName);
        [DllImport("Advapi32.dll")]
        public static extern IntPtr OpenEventLog(string ServerName, string LogFileType);

        public Dictionary<string, StringBuilder> InfoStrings { get { return infoStrings; } }
        Dictionary<string, StringBuilder> infoStrings = new Dictionary<string, StringBuilder>();

        private StringBuilder sysInfo = new StringBuilder();
        private StringBuilder folderInfo = new StringBuilder();
        private StringBuilder FireWallInfo = new StringBuilder();
        private StringBuilder ServiceInfo = new StringBuilder();
        private StringBuilder CPUInfo = new StringBuilder();
        private StringBuilder MemoryInfo = new StringBuilder();

        private static DateTime StartTime = new DateTime();
        public static Compress Compresser = new Compress();

        public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler WorkDone;

        public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs fe);
        public event ProgressChangedEventHandler ProgressChanged;

        private void OnWorkDone()
        {
            if (WorkDone != null)
                WorkDone(this, new EventArgs());
        }

        private void OnProgressChanged(int value, string capture)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new ProgressChangedEventArgs(value, capture));
            }
        }

        public Gatherer()
        {

        }

        public void TakeInfos()
        {
            StartTime = DateTime.Now;
            FolderTrace ft = new FolderTrace();

            infoStrings.Clear();
            sysInfo.Clear();
            folderInfo.Clear();
            FireWallInfo.Clear();
            ServiceInfo.Clear();
            CPUInfo.Clear();
            MemoryInfo.Clear();

            try
            {
                OnProgressChanged(0, Properties.Settings.Default.Progress_prepearing);
                Compresser.CreateFile(System.IO.Path.GetTempPath() + "\\ParsecConfig.zip");

                NewSection(ref sysInfo, 1, Properties.Settings.Default.Progress_OSInfo);
                sysInfo.AppendLine(OSVersion.GetOSVerson());

                NewSection(ref FireWallInfo, 2, Properties.Settings.Default.Progress_FireWal);
                try
                {
                    FireWall.GetFullInfo(ref FireWallInfo);
                }
                catch (Exception e)
                {
                    FireWallInfo.AppendLine("Error: " + e.ToString());
                }
                infoStrings.Add("FireWallInfo", FireWallInfo);

                NewSection(ref ServiceInfo, 7, Properties.Settings.Default.Progress_Services);
                GetServices(ref ServiceInfo);
                infoStrings.Add("ServiceInfo", ServiceInfo);

                NewSection(ref sysInfo, 12, Properties.Settings.Default.Progress_Domain);
                sysInfo.AppendLine("Domain: " + Environment.UserDomainName);

                NewSection(ref sysInfo, 13, Properties.Settings.Default.Progress_DHCP);
                GetDHCP();

                NewSection(ref sysInfo, 20, Properties.Settings.Default.Progress_Saving + ": " + 
                    Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MDO\ParsecNET 3");
                SaveFolder(ft, ref sysInfo);

                NewSection(ref CPUInfo, 30, "Getting CPU info");
                WMIParser.parse("Win32_Processor", ref CPUInfo);
                infoStrings.Add("CPUInfo", CPUInfo);

                NewSection(ref MemoryInfo, 30, "Getting memory info");
                WMIParser.parse("Win32_PhysicalMemory", ref MemoryInfo);
                infoStrings.Add("MemoryInfo", MemoryInfo);

                NewSection(ref folderInfo, 40, Properties.Settings.Default.Progress_Registry);
                GetRegistry(ft, ref folderInfo);
                infoStrings.Add("FolderInfo", folderInfo);  

                NewSection(ref sysInfo, 60, Properties.Settings.Default.Progress_Saving + " Application Event Log");
                SaveEventLog("Application");

                NewSection(ref sysInfo, 80, Properties.Settings.Default.Progress_Saving + " System Event Log");
                SaveEventLog("System"); 
            }
            catch
            {
                sysInfo.AppendLine(Properties.Settings.Default.Progress_doneWithError);
            }
            finally
            {
                NewSection(ref sysInfo, 99, Properties.Settings.Default.Progress_Saving + " Logs"); 

                foreach (KeyValuePair<string, StringBuilder> log in infoStrings)
                {
                    if (log.Key != "SysInfo")
                        try
                        {
                            SaveString(Path.GetTempPath() + log.Key + ".txt", log.Value.ToString());
                            sysInfo.AppendLine(log.Key + " " + Properties.Settings.Default.Progress_Saved);
                        }
                        catch
                        {
                            sysInfo.AppendLine(log.Key + " " + Properties.Settings.Default.Progress_SaveError);
                        }
                }

                try
                {
                    SaveString(Path.GetTempPath() + "SysInfo.txt", sysInfo.ToString());
                    sysInfo.AppendLine("SysInfo " + Properties.Settings.Default.Progress_Saved);
                }
                catch
                {
                    sysInfo.AppendLine("SysInfo " + Properties.Settings.Default.Progress_SaveError);
                }

                infoStrings.Add("SysInfo", sysInfo);

                Compresser.CloseFile();
                OnProgressChanged(100, Properties.Settings.Default.Progress_done);                
                OnWorkDone();
            }
        }

        private void SaveString(string path, string source)
        {
            File.WriteAllText(path, source, Encoding.UTF8);
            Compresser.AddFile(path);
        }

        private void NewSection(ref StringBuilder sb, int progress, string capture)
        {
            OnProgressChanged(progress, capture);
            sb.AppendLine("-----------------------------------------------------------------------------");
        }

        private void SaveEventLog(string logType)
        {
            IntPtr EventLogHandle;
            string localPC = null; // or other computer's UNC name.

            string f = Path.GetTempPath() + logType + "EventLog.evt";
            try
            {
                EventLogHandle = OpenEventLog(localPC, logType);
                BackupEventLog(EventLogHandle, f);
                Compresser.AddFile(f);
                sysInfo.AppendLine(logType + " log finded");
            }
            catch
            {
                sysInfo.AppendLine(logType + " log find error.");
            }
        }

        private void GetRegistry(FolderTrace ft, ref StringBuilder outStr)
        {
            string iPath = "";
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(Properties.Settings.Default.RegistryKeyLoc, false);
                iPath = key.GetValue(Properties.Settings.Default.RegistryKeyValue).ToString();
                outStr.AppendLine(Properties.Settings.Default.RegistryKeyValue + " = " + iPath);
            }
            catch (Exception e)
            {
                outStr.AppendLine("Registry key is unavailable");
                outStr.AppendLine(e.ToString());
                return;
            }
            try
            {
                NewSection(ref folderInfo, 50, Properties.Settings.Default.Progress_folderTrace);
                System.Collections.Generic.List<string> result = ft.TraceFolder(iPath, "InstallDirectory", " ");
                foreach (string str in result)
                    outStr.AppendLine(str);
            }
            catch (Exception e)
            {
                outStr.AppendLine("TraceFolder Error: " + e.ToString());
            }
        }

        private void SaveFolder(FolderTrace ft, ref StringBuilder outStr)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\MDO\ParsecNET 3";
            try
            {
                ft.CompressFolder(path, "ProgrammData");
                outStr.AppendLine(path + Properties.Settings.Default.Progress_Saved);
            }
            catch (Exception e)
            {
                outStr.AppendLine(path + Properties.Settings.Default.Progress_SaveError);
                outStr.AppendLine(e.Message);
            }
        }

        private void GetDHCP()
        {
            sysInfo.AppendLine("DHCP:");
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in nics)
                {
                    if (ni.OperationalStatus == OperationalStatus.Up)
                    {
                        IPAddressCollection ips = ni.GetIPProperties().DnsAddresses;
                        foreach (System.Net.IPAddress ip in ips)
                        {
                            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                sysInfo.AppendLine("  " + ip.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                sysInfo.AppendLine("Error: " + e.ToString());
            }
        }

        private void GetServices(ref StringBuilder sb)
        {
            try
            {
                sb.AppendLine("Service info:\n");
                ServiceController[] services = ServiceController.GetServices();
                foreach (ServiceController service in services)
                {
                    sb.AppendLine(" - " + service.DisplayName + " (" + service.ServiceName + ") " + service.Status);
                }

            }
            catch (Exception e)
            {
                sb.AppendLine("Error: " + e.ToString());
            }
        }

    }
}
