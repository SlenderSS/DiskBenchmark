using DiskBenchmark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.Services
{
    internal class DisksList
    {
        public static void GetDisks()
        {
            List<Disk> disks = new List<Disk>();
            ManagementScope scope = new ManagementScope("\\\\.\\root\\CIMV2");

            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_DiskDrive");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

            foreach (ManagementObject disk in searcher.Get())
            {
                Disk newDisk = new Disk();
                newDisk.Caption = disk["Caption"].ToString();
                newDisk.DeviceID = disk["DeviceID"].ToString();
                newDisk.SerialNumber = disk["SerialNumber"].ToString();
                newDisk.Size = long.Parse(disk["Size"].ToString());
                // newDisk.FreeSpace = long.Parse(disk["Free Space"].ToString());

                ObjectQuery partitionQuery = new ObjectQuery($"ASSOCIATORS OF {{Win32_DiskDrive.DeviceID='{newDisk.DeviceID}'}} WHERE AssocClass = Win32_DiskDriveToDiskPartition");
                ManagementObjectSearcher partitionSearcher = new ManagementObjectSearcher(scope, partitionQuery);

                foreach (ManagementObject partition in partitionSearcher.Get())
                {
                    Partition newPartition = new Partition();
                    newPartition.Caption = partition["Caption"].ToString();
                    newPartition.DeviceID = partition["DeviceID"].ToString();
                    newPartition.Size = long.Parse(partition["Size"].ToString());

                    ObjectQuery logicalDiskQuery = new ObjectQuery($"ASSOCIATORS OF {{Win32_DiskPartition.DeviceID='{newPartition.DeviceID}'}} WHERE AssocClass = Win32_LogicalDiskToPartition");
                    ManagementObjectSearcher logicalDiskSearcher = new ManagementObjectSearcher(scope, logicalDiskQuery);

                    foreach (ManagementObject logicalDisk in logicalDiskSearcher.Get())
                    {
                        LogicalDisk newLogicalDisk = new LogicalDisk();
                        newLogicalDisk.Caption = logicalDisk["Caption"].ToString();
                        newLogicalDisk.DeviceID = logicalDisk["DeviceID"].ToString();
                        newLogicalDisk.FileSystem = logicalDisk["FileSystem"].ToString();
                        newLogicalDisk.FreeSpace = long.Parse(logicalDisk["FreeSpace"].ToString());
                        newLogicalDisk.Size = long.Parse(logicalDisk["Size"].ToString());

                        newPartition.LogicalDisks.Add(newLogicalDisk);
                    }

                    newDisk.Partitions.Add(newPartition);
                }

                disks.Add(newDisk);
            }
        }
    }
}
