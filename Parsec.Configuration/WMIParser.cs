using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace Parsec.Configuration
{
    class WMIParser
    {
        static public void parse(string key, ref StringBuilder SBOut)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + key);
            string meaning;
            try
            {
                foreach (ManagementObject share in searcher.Get())
                {
                    try
                    {
                        SBOut.AppendLine("================" + share["Name"].ToString() + "================");
                    }
                    catch
                    {
                        SBOut.AppendLine("================" + share.ToString() + "================");
                    }

                    if (share.Properties.Count <= 0)
                    {
                        SBOut.AppendLine("Error, No Information Available");
                        return;
                    }

                    foreach (PropertyData PC in share.Properties)
                    {
                        if (PC.Value != null && PC.Value.ToString() != "")
                            if ((meaning = GetMeaming(PC.Name, PC.Value.ToString())) != "")
                                SBOut.AppendLine(PC.Name + " = " + meaning + " (" + PC.Value.ToString() + ")");
                            else
                                SBOut.AppendLine(PC.Name + " = " + PC.Value.ToString());
                        else
                            SBOut.AppendLine(PC.Name + " = " + "No Information available");
                    }
                }
            }
            catch (Exception exp)
            {
                SBOut.AppendLine("Error, can't get data because of the following error \n" + exp.Message);
            }
        }

        static public string GetMeaming(string key, string val)
        {
            switch (key)
            {
                case "Architecture":
                    switch (val)
                    {
                        case "0":
                            return "x32";
                        case "1":
                            return "MIPS";
                        case "2":
                            return "Alpha";
                        case "3":
                            return "PowerPC";
                        case "6":
                            return "Itanium-based systems";
                        case "9":
                            return "x64";
                        default:
                            return "";
                    }
                case "Availability":
                    switch (val)
                    {
                        case "1":
                            return "Other";
                        case "2":
                            return "Unknown";
                        case "3":
                            return "Running or Full Power";
                        case "4":
                            return "Warning";
                        case "5":
                            return "In Test";
                        case "6":
                            return "Not Applicable";
                        case "7":
                            return "Power Off";
                        case "8":
                            return "Off Line";
                        case "9":
                            return "Off Duty";
                        case "10":
                            return "Degraded";
                        case "11":
                            return "Not Installed";
                        case "12":
                            return "Install Error";
                        case "13":
                            return "Power Save - Unknown\nThe device is known to be in a power save state, but its exact status is unknown.";
                        case "14":
                            return "Power Save - Low Power Mode\nThe device is in a power save state, but is still functioning, and may exhibit decreased performance.";
                        case "15":
                            return "Power Save - Standby\nThe device is not functioning, but can be brought to full power quickly.";
                        case "16":
                            return "Power Cycle";
                        case "17":
                            return "Power Save - Warning\nThe device is in a warning state, though also in a power save state.";
                        default:
                            return "";
                    }
                case "ConfigManagerErrorCode":
                    switch (val)
                    {
                        case "0":
                            return "Device is working properly";
                        case "1":
                            return "Device is not configured correctly.";
                        case "2":
                            return "Windows cannot load the driver for this device";
                        case "3":
                            return "Driver for this device might be corrupted or the system may be low on memory or other resources.";
                        case "4":
                            return "Device is not working properly. One of its drivers or the registry might be corrupted.";
                        case "5":
                            return "Driver for the device requires a resource that Windows cannot manage.";
                        case "6":
                            return "Boot configuration for the device conflicts with other devices.";
                        case "7":
                            return "Cannot filter.";
                        case "8":
                            return "Driver loader for the device is missing.";
                        case "9":
                            return "Device is not working properly. The controlling firmware is incorrectly reporting the resources for the device.";
                        case "10":
                            return "Device cannot start.";
                        case "11":
                            return "Device failed.";
                        case "12":
                            return "Device cannot find enough free resources to use.";
                        case "13":
                            return "Windows cannot verify the device's resources.";
                        case "14":
                            return "Device cannot work properly until the computer is restarted.";
                        case "15":
                            return "Device is not working properly due to a possible re-enumeration problem.";
                        case "16":
                            return "Windows cannot identify all of the resources that the device uses.";
                        case "17":
                            return "Device is requesting an unknown resource type.";
                        case "18":
                            return "Device drivers must be reinstalled.";
                        case "19":
                            return "Failure using the VxD loader.";
                        case "20":
                            return "Registry might be corrupted.";
                        case "21":
                            return "System failure. If changing the device driver is ineffective, see the hardware documentation. Windows is removing the device.";
                        case "22":
                            return "Device is disabled.";
                        case "23":
                            return "System failure. If changing the device driver is ineffective, see the hardware documentation.";
                        case "24":
                            return "Device is not present, not working properly, or does not have all of its drivers installed.";
                        case "25":
                            return "Windows is still setting up the device.";
                        case "26":
                            return "Windows is still setting up the device.";
                        case "27":
                            return "Device does not have valid log configuration.";
                        case "28":
                            return "Device drivers are not installed.";
                        case "29":
                            return "Device is disabled. The device firmware did not provide the required resources.";
                        case "30":
                            return "Device is using an IRQ resource that another device is using.";
                        case "31":
                            return "Device is not working properly. Windows cannot load the required device drivers";                        
                        default:
                            return "";
                    }
                case "CpuStatus":
                    switch (val)
                    {
                        case "0":
                            return "Unknown";
                        case "1":
                            return "CPU Enabled";
                        case "2":
                            return "CPU Disabled by User via BIOS Setup";
                        case "3":
                            return "CPU Disabled by BIOS (POST Error)";
                        case "4":
                            return "CPU Is Idle";
                        case "5":
                            return "Reserved";
                        case "6":
                            return "Reserved";
                        case "7":
                            return "Other";                        
                        default:
                            return "";
                    }
                case "Family":
                    switch (val)
                    {
                        case "1":
                            return "Other";
                        case "2":
                            return "Unknown";
                        case "3":
                            return "8086";
                        case "4":
                            return "80286";
                        case "5":
                            return "Intel386™ Processor";
                        case "6":
                            return "Intel486™ Processor";
                        case "7":
                            return "8087";
                        case "8":
                            return "80287";
                        case "9":
                            return "80387";
                        case "10":
                            return "80487";
                        case "11":
                            return "Pentium Brand";
                        case "12":
                            return "Pentium Pro";
                        case "13":
                            return "Pentium II";
                        case "14":
                            return "Pentium Processor with MMX™ Technology";
                        case "15":
                            return "Celeron™";
                        case "16":
                            return "Pentium II Xeon™";
                        case "17":
                            return "Pentium III";
                        case "18":
                            return "M1 Family";
                        case "19":
                            return "M2 Family";             
                        case "24":
                            return "AMD Duron™ Processor Family";
                        case "25":
                            return "K5 Family";
                        case "26":
                            return "K6 Family";
                        case "27":
                            return "K6-2";
                        case "28":
                            return "K6-3";
                        case "29":
                            return "AMD Athlon™ Processor Family";
                        case "30":
                            return "AMD2900 Family";
                        case "31":
                            return "K6-2+";
                        case "32":
                            return "Power PC Family";
                        case "33":
                            return "Power PC 601";
                        case "34":
                            return "Power PC 603";
                        case "35":
                            return "Power PC 603+";
                        case "36":
                            return "Power PC 604";
                        case "37":
                            return "Power PC 620";
                        case "38":
                            return "Power PC X704";
                        case "39":
                            return "Power PC 750";
                        case "48":
                            return "Alpha Family";
                        case "49":
                            return "Alpha 21064";
                        case "50":
                            return "Alpha 21066";
                        case "51":
                            return "Alpha 21164";
                        case "52":
                            return "Alpha 21164PC";
                        case "53":
                            return "Alpha 21164a";
                        case "54":
                            return "Alpha 21264";
                        case "55":
                            return "Alpha 21364";
                        case "64":
                            return "MIPS Family";
                        case "65":
                            return "MIPS R4000";
                        case "66":
                            return "MIPS R4200";
                        case "67":
                            return "MIPS R4400";
                        case "68":
                            return "MIPS R4600";
                        case "69":
                            return "MIPS R10000";
                        case "80":
                            return "SPARC Family";
                        case "81":
                            return "SuperSPARC";
                        case "82":
                            return "microSPARC II";
                        case "83":
                            return "microSPARC IIep";
                        case "84":
                            return "UltraSPARC";
                        case "85":
                            return "UltraSPARC II";
                        case "86":
                            return "UltraSPARC IIi";
                        case "87":
                            return "UltraSPARC III";
                        case "88":
                            return "UltraSPARC IIIi";                      
                        case "96":
                            return "68040";
                        case "97":
                            return "68xxx Family";
                        case "98":
                            return "68000";
                        case "99":
                            return "68010";
                        case "100":
                            return "68020";
                        case "101":
                            return "68030";
                        case "112":
                            return "Hobbit Family";
                        case "120":
                            return "Crusoe™ TM5000 Family";
                        case "121":
                            return "Crusoe™ TM3000 Family";
                        case "122":
                            return "Efficeon™ TM8000 Family";
                        case "128":
                            return "Weitek";
                        case "130":
                            return "Itanium™ Processor";
                        case "131":
                            return "AMD Athlon™ 64 Processor Famiily";
                        case "132":
                            return "AMD Opteron™ Processor Family";
                        case "144":
                            return "PA-RISC Family";
                        case "145":
                            return "PA-RISC 8500";
                        case "146":
                            return "PA-RISC 8000";
                        case "147":
                            return "PA-RISC 7300LC";
                        case "148":
                            return "PA-RISC 7200";
                        case "149":
                            return "PA-RISC 7100LC";
                        case "150":
                            return "PA-RISC 7100";
                        case "160":
                            return "V30 Family";
                        case "176":
                            return "Pentium III Xeon™ Processor";
                        case "177":
                            return "Pentium III Processor with Intel SpeedStep™ Technology";
                        case "178":
                            return "Pentium 4";
                        case "179":
                            return "Intel Xeon™";
                        case "180":
                            return "AS400 Family";
                        case "181":
                            return "Intel Xeon™ Processor MP";
                        case "182":
                            return "AMD Athlon™ XP Family";
                        case "183":
                            return "AMD Athlon™ MP Family";
                        case "184":
                            return "Intel Itanium 2";
                        case "185":
                            return "Intel Pentium M Processor";
                        case "190":
                            return "K7";
                        case "200":
                            return "IBM390 Family";
                        case "201":
                            return "G4";
                        case "202":
                            return "G5";
                        case "203":
                            return "G6";
                        case "204":
                            return "z/Architecture Base";
                        case "250":
                            return "i860";
                        case "251":
                            return "i960";
                        case "260":
                            return "SH-3";
                        case "261":
                            return "SH-4";
                        case "280":
                            return "ARM";
                        case "281":
                            return "StrongARM";
                        case "300":
                            return "6x86";
                        case "301":
                            return "MediaGX";
                        case "302":
                            return "MII";
                        case "320":
                            return "WinChip";
                        case "350":
                            return "DSP";
                        case "500":
                            return "Video Processor";
                        default:
                            return "";
                    }
                case "PowerManagementCapabilities":
                    switch (val)
                    {
                        case "0":
                            return "Unknown";
                        case "1":
                            return "Not Supported";
                        case "2":
                            return "Disabled";
                        case "3":
                            return "Enabled\nThe power management features are currently enabled but the exact feature set is unknown or the information is unavailable.";
                        case "4":
                            return "Power Saving Modes Entered Automatically\nThe device can change its power state based on usage or other criteria";
                        case "5":
                            return "Power State Settable\nThe SetPowerState method is supported. This method is found on the parent CIM_LogicalDevice class and can be implemented. For more information, see Designing Managed Object Format (MOF) Classes.";
                        case "6":
                            return "Power Cycling Supported\nThe SetPowerState method can be invoked with the PowerState parameter set to 5 (Power Cycle).";
                        case "7":
                            return "Timed Power-On Supported\nThe SetPowerState method can be invoked with the PowerState parameter set to 5 (Power Cycle) and Time set to a specific date and time, or interval, for power-on.";                        
                        default:
                            return "";
                    }
                case "ProcessorType":
                    switch (val)
                    {
                        case "1":
                            return "Other";
                        case "2":
                            return "Unknown";
                        case "3":
                            return "Central Processor";
                        case "4":
                            return "Math Processor";
                        case "5":
                            return "DSP Processor";
                        case "6":
                            return "Video Processor";                        
                        default:
                            return "";
                    }
                case "StatusInfo":
                    switch (val)
                    {                       
                        case "1":
                            return "Other";
                        case "2":
                            return "Unknown";
                        case "3":
                            return "Enabled";
                        case "4":
                            return "Disabled";
                        case "5":
                            return "Not Applicable";                        
                        default:
                            return "";
                    }
                case "UpgradeMethod":
                    switch (val)
                    {
                        case "1":
                            return "Other";
                        case "2":
                            return "Unknown";
                        case "3":
                            return "Daughter Board";
                        case "4":
                            return "ZIF Socket";
                        case "5":
                            return "Replacement or Piggy Back";
                        case "6":
                            return "None";
                        case "7":
                            return "LIF Socket";
                        case "8":
                            return "Slot 1";
                        case "9":
                            return "Slot 2";
                        case "10":
                            return "370 Pin Socket";
                        case "11":
                            return "Slot A";
                        case "12":
                            return "Slot M";
                        case "13":
                            return "Socket 423";
                        case "14":
                            return "Socket A (Socket 462)";
                        case "15":
                            return "Socket 478";
                        case "16":
                            return "Socket 754";
                        case "17":
                            return "Socket 940";
                        case "18":
                            return "Socket 939";                        
                        default:
                            return "";
                    }
                case "VoltageCaps":
                    switch (val)
                    {
                        case "1":
                            return "5 volts";
                        case "2":
                            return "3.3 volts";
                        case "3":
                            return "2.9 volts";                       
                        default:
                            return "";
                    }                
                default:
                    return "";
            }
        }
    }
}
