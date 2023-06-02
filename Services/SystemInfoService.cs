using DiskBenchmark.Models;
using Microsoft.Win32;
using System.Management;

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
                    sysInfo.SystemName =  (obj["Caption"] != null ? obj["Caption"].ToString() : "None");   //Display operating system caption
                }
                if (obj["OSArchitecture"] != null)
                {
                     sysInfo.OSArchitecture =   (obj["OSArchitecture"] != null ? obj["OSArchitecture"].ToString() : "None");   //Display operating system architecture.
                }
                
                sysInfo.CPUName =  GetProcessorInfo();
            }


            SelectQuery query = new SelectQuery(@"Select * from Win32_ComputerSystem");

           
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
               
                foreach (ManagementObject obj in searcher.Get())
                {
                    sysInfo.OSManufacturer =  (obj["Manufacturer"] != null ? obj["Manufacturer"].ToString() : "None");
                    sysInfo.Model =  (obj["Model"] != null ? obj["Model"].ToString() : "None");
                }
            }
            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher("SELECT * FROM Win32_BIOS");
           
            foreach (ManagementObject obj in searcher1.Get())
            {
                if (((string[])obj["BIOSVersion"]).Length > 1)
                    sysInfo.BIOSVersion = ((string[])obj["BIOSVersion"])[0] + " - " + ((string[])obj["BIOSVersion"])[1];
                else
                    sysInfo.BIOSVersion =  ((string[])obj["BIOSVersion"])[0];
            }
            using (var searcher = new ManagementObjectSearcher("select * from Win32_VideoController"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    sysInfo.GPUName = obj["Name"].ToString();
                   
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
