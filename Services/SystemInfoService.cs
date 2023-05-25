using DiskBenchmark.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.Services
{
    class SystemInfoService
    {
        public SystemInfo GetOperatingSystemInfo()
        {
            var sysInfo = new SystemInfo();
            ManagementObjectSearcher mos = new ManagementObjectSearcher("select * from Win32_OperatingSystem");
            foreach (ManagementObject obj in mos.Get())
            {
                if (obj["Caption"] != null)
                {
                    sysInfo.SystemName = "Operating System: " + (obj["Caption"] != null ? obj["Caption"].ToString() : "None");   //Display operating system caption
                }
                if (obj["OSArchitecture"] != null)
                {
                     sysInfo.OSArchitecture =  "Architecture: " + (obj["OSArchitecture"] != null ? obj["OSArchitecture"].ToString() : "None");   //Display operating system architecture.
                }
                
                sysInfo.CPUName = "CPU name: " + GetProcessorInfo();
            }


            SelectQuery query = new SelectQuery(@"Select * from Win32_ComputerSystem");

           
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
               
                foreach (ManagementObject obj in searcher.Get())
                {
                    sysInfo.OSManufacturer = "System Manufacturer: " + (obj["Manufacturer"] != null ? obj["Manufacturer"].ToString() : "None");
                    sysInfo.Model = "System Model: " + (obj["Model"] != null ? obj["Model"].ToString() : "None");
                }
            }
            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
           
            foreach (ManagementObject obj in searcher1.Get())
            {
                if (((string[])obj["BIOSVersion"]).Length > 1)
                    sysInfo.BIOSVersion = "BIOS VERSION: " + ((string[])obj["BIOSVersion"])[0] + " - " + ((string[])obj["BIOSVersion"])[1];
                else
                    sysInfo.BIOSVersion =  "BIOS VERSION: " + ((string[])obj["BIOSVersion"])[0];
            }
            using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    sysInfo.GPUName = "GPUName: " + obj["Name"];
                   
                }
            }

            return sysInfo;
        }

        private string GetProcessorInfo()
        {
            
            RegistryKey processor_name = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);   //This registry entry contains entry for processor info.

            if (processor_name != null)
            {
                if (processor_name.GetValue("ProcessorNameString") != null)
                {
                    return processor_name.GetValue("ProcessorNameString").ToString();   //Display processor ingo.
                }
            }
            return "No information...";
            
        }
    }



}
