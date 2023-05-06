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
            foreach (ManagementObject managementObject in mos.Get())
            {
                if (managementObject["Caption"] != null)
                {
                    sysInfo.SystemName = "Operating System: " + managementObject["Caption"].ToString();   //Display operating system caption
                }
                if (managementObject["OSArchitecture"] != null)
                {
                     sysInfo.OSArchitecture =  "Architecture: " + managementObject["OSArchitecture"].ToString();   //Display operating system architecture.
                }
                
                sysInfo.CPUName = "CPU name: " + GetProcessorInfo();
            }


            SelectQuery query = new SelectQuery(@"Select * from Win32_ComputerSystem");

           
            using (ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher(query))
            {
                //execute the query
                foreach (ManagementObject process in searcher.Get())
                {
                    
                   
                    sysInfo.OSManufacturer = "System Manufacturer: " + process["Manufacturer"] != null ? process["Manufacturer"].ToString() : "None";
                    sysInfo.Model = "System Model: " + process["Model"] != null ? process["Model"].ToString() : "None";


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
