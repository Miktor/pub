using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Parsec.Configuration
{
    class OSVersion
    {
        [DllImport("kernel32")]
        static extern bool GetVersionEx(ref OSVERSIONINFO osvi);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll")]
        static extern void GetSystemInfo(out SYSTEM_INFO lpSystemInfo);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int smIndex);

        struct OSVERSIONINFO
        {
            public uint dwOSVersionInfoSize;
            public uint dwMajorVersion;
            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public Int16 wServicePackMajor;
            public Int16 wServicePackMinor;
            public Int16 wSuiteMask;
            public Byte wProductType;
            public Byte wReserved;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEM_INFO
        {
            public ushort processorArchitecture;
            ushort reserved;
            public uint pageSize;
            public IntPtr minimumApplicationAddress;
            public IntPtr maximumApplicationAddress;
            public IntPtr activeProcessorMask;
            public uint numberOfProcessors;
            public uint processorType;
            public uint allocationGranularity;
            public ushort processorLevel;
            public ushort processorRevision;
        }


        const ushort PROCESSOR_ARCHITECTURE_INTEL = 0;
        const ushort PROCESSOR_ARCHITECTURE_IA64 = 6;
        const ushort PROCESSOR_ARCHITECTURE_AMD64 = 9;
        const ushort PROCESSOR_ARCHITECTURE_UNKNOWN = 0xFFFF;  

        const int VER_NT_WORKSTATION = 1;
        const int VER_NT_DOMAIN_CONTROLLER = 2;
        const int VER_NT_SERVER = 3;
        const ushort VER_PLATFORM_WIN32_NT = 2;
        const int VER_SUITE_SMALLBUSINESS = 1;
        const int VER_SUITE_ENTERPRISE = 2;
        const int VER_SUITE_TERMINAL = 16;
        const int VER_SUITE_DATACENTER = 128;
        const int VER_SUITE_SINGLEUSERTS = 256;
        const int VER_SUITE_PERSONAL = 512;
        const int VER_SUITE_BLADE = 1024;
        const int VER_SUITE_COMPUTE_SERVER = 0x00004000;
        const int VER_SUITE_STORAGE_SERVER = 0x00002000;
        const int VER_SUITE_WH_SERVER = 0x00008000;

        const int SM_SERVERR2 = 89;



        public static string GetOSVerson()
        {
            OSVERSIONINFO osvi;
            SYSTEM_INFO si;

            string vers = string.Empty;
            try
            {
                osvi = new OSVERSIONINFO();
                osvi.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osvi);

                if (GetVersionEx(ref osvi) == false) throw new Exception();

                GetSystemInfo(out si);

                if (VER_PLATFORM_WIN32_NT == osvi.dwPlatformId &&
                osvi.dwMajorVersion > 4)
                {
                    if (osvi.dwMajorVersion == 6)
                    {
                        if (osvi.dwMinorVersion == 0)
                        {
                            if (osvi.wProductType == VER_NT_WORKSTATION)
                                vers += "Windows Vista ";
                            else
                                vers += "Windows Server 2008 ";
                        }

                        if (osvi.dwMinorVersion == 1)
                        {
                            if (osvi.wProductType == VER_NT_WORKSTATION)
                                vers += "Windows 7 ";
                            else
                                vers += "Windows Server 2008 R2 ";
                        }
                    }

                    if (osvi.dwMajorVersion == 5 && osvi.dwMinorVersion == 2)
                    {
                        if (GetSystemMetrics(SM_SERVERR2) != 0)
                            vers += "Windows Server 2003 R2, ";
                        else if (osvi.wSuiteMask == VER_SUITE_STORAGE_SERVER)
                            vers += "Windows Storage Server 2003";
                        else if (osvi.wSuiteMask == VER_SUITE_WH_SERVER)
                            vers += "Windows Home Server";
                        else if (osvi.wProductType == VER_NT_WORKSTATION && si.processorArchitecture == PROCESSOR_ARCHITECTURE_AMD64)
                            vers += "Windows XP Professional x64 Edition";
                        else
                            vers += "Windows Server 2003, ";

                        // Test for the server type.
                        if (osvi.wProductType != VER_NT_WORKSTATION)
                        {
                            if (si.processorArchitecture == PROCESSOR_ARCHITECTURE_IA64)
                            {
                                if (osvi.wSuiteMask == VER_SUITE_DATACENTER)
                                    vers += "Datacenter Edition for Itanium-based Systems";
                                else if (osvi.wSuiteMask == VER_SUITE_ENTERPRISE)
                                    vers += "Enterprise Edition for Itanium-based Systems";
                            }

                            else if (si.processorArchitecture == PROCESSOR_ARCHITECTURE_AMD64)
                            {
                                if (osvi.wSuiteMask == VER_SUITE_DATACENTER)
                                    vers += "Datacenter x64 Edition";
                                else if (osvi.wSuiteMask == VER_SUITE_ENTERPRISE)
                                    vers += "Enterprise x64 Edition";
                                else vers += "Standard x64 Edition";
                            }

                            else
                            {
                                if (osvi.wSuiteMask == VER_SUITE_COMPUTE_SERVER)
                                    vers += "Compute Cluster Edition";
                                else if (osvi.wSuiteMask == VER_SUITE_DATACENTER)
                                    vers += "Datacenter Edition";
                                else if (osvi.wSuiteMask == VER_SUITE_ENTERPRISE)
                                    vers += "Enterprise Edition";
                                else if (osvi.wSuiteMask == VER_SUITE_BLADE)
                                    vers += "Web Edition";
                                else vers += "Standard Edition";
                            }
                        }
                    }

                    if (osvi.dwMajorVersion == 5 && osvi.dwMinorVersion == 1)
                    {
                        vers += "Windows XP ";
                        if (osvi.wSuiteMask == VER_SUITE_PERSONAL)
                            vers += "Home Edition";
                        else vers += "Professional";
                    }

                    if (osvi.dwMajorVersion == 5 && osvi.dwMinorVersion == 0)
                    {
                        vers += "Windows 2000 ";

                        if (osvi.wProductType == VER_NT_WORKSTATION)
                        {
                            vers += "Professional";
                        }
                        else
                        {
                            if (osvi.wSuiteMask == VER_SUITE_DATACENTER)
                                vers += "Datacenter Server";
                            else if (osvi.wSuiteMask == VER_SUITE_ENTERPRISE)
                                vers += "Advanced Server";
                            else vers += "Server";
                        }
                    }
                    
                    if (osvi.dwMajorVersion >= 6)
                    {
                        if (Environment.Is64BitOperatingSystem)
                            vers += " 64-bit";
                        else
                            vers += " 32-bit";
                    }
                    return vers + Environment.OSVersion.ServicePack;
                }
                return vers;
            }
            catch (Exception e)
            {
                return Marshal.GetLastWin32Error().ToString();
            }
            finally
            {

            }
        }
    }
}
