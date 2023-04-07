using DiskBenchmark.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.Services
{
    internal class DisksListService
    {




        public IEnumerable<Disk> GetDisks()
        {
                //IEnumerable<Disk> disks = new IEnumerable<Disk>();
                ManagementScope scope = new ManagementScope("\\\\.\\root\\CIMV2");

                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_DiskDrive");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                foreach (ManagementObject disk in searcher.Get())
                {
                    Disk newDisk = new Disk();
                    newDisk.Caption = disk["Model"].ToString();
                    newDisk.DeviceID = disk["DeviceID"].ToString();
                    newDisk.SerialNumber = disk["SerialNumber"].ToString();
                    newDisk.Size = long.Parse(disk["Size"].ToString());
                   

                    ObjectQuery partitionQuery = new ObjectQuery($"ASSOCIATORS OF {{Win32_DiskDrive.DeviceID='{newDisk.DeviceID}'}} WHERE AssocClass = Win32_DiskDriveToDiskPartition");
                    ManagementObjectSearcher partitionSearcher = new ManagementObjectSearcher(scope, partitionQuery);

                    foreach (ManagementObject partition in partitionSearcher.Get())
                    {

                        ObjectQuery logicalDiskQuery = new ObjectQuery($"ASSOCIATORS OF {{Win32_DiskPartition.DeviceID='{partition["DeviceID"]}'}} WHERE AssocClass = Win32_LogicalDiskToPartition");
                        ManagementObjectSearcher logicalDiskSearcher = new ManagementObjectSearcher(scope, logicalDiskQuery);

                        foreach (ManagementObject logicalDisk in logicalDiskSearcher.Get())
                        {
                            LogicalDisk newLogicalDisk = new LogicalDisk();
                            newLogicalDisk.Caption = logicalDisk["Caption"].ToString();
                            newLogicalDisk.DeviceID = logicalDisk["DeviceID"].ToString();
                            newLogicalDisk.FileSystem = logicalDisk["FileSystem"].ToString();
                            newLogicalDisk.Size = long.Parse(logicalDisk["Size"].ToString());
                            newLogicalDisk.UsedSpace = newLogicalDisk.Size - long.Parse(logicalDisk["FreeSpace"].ToString());
                            newDisk.TotalUsedSpace += newLogicalDisk.UsedSpace;
                            newDisk.LogicalDisks.Add(newLogicalDisk);
                        }
                    }
                yield return newDisk;      /*     disks.Add(newDisk);*/
                }
                //return disks;
            
        }
    }
}
