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
    internal class DisksList
    {

        private static IEnumerable<LogicalDisk> GetLogicalDisks(Partition partition, ManagementScope scope)
        {
            ObjectQuery logicalDiskQuery = new ObjectQuery($"ASSOCIATORS OF {{Win32_DiskPartition.DeviceID='{partition.DeviceID}'}} WHERE AssocClass = Win32_LogicalDiskToPartition");
            ManagementObjectSearcher logicalDiskSearcher = new ManagementObjectSearcher(scope, logicalDiskQuery);
            
            foreach (ManagementObject logicalDisk in logicalDiskSearcher.Get())
            {
                LogicalDisk newLogicalDisk = new LogicalDisk();
                newLogicalDisk.Caption = logicalDisk["Caption"].ToString();
                newLogicalDisk.DeviceID = logicalDisk["DeviceID"].ToString();
                newLogicalDisk.FileSystem = logicalDisk["FileSystem"].ToString();
                newLogicalDisk.FreeSpace = long.Parse(logicalDisk["FreeSpace"].ToString());
                newLogicalDisk.Size = long.Parse(logicalDisk["Size"].ToString());

                yield return newLogicalDisk;
                partition.LogicalDisks.Add(newLogicalDisk);

            }
           
        }

        private static IEnumerable<Partition> GetPartitions(Disk disk, ManagementScope scope)
        {
            ObjectQuery partitionQuery = new ObjectQuery($"ASSOCIATORS OF {{Win32_DiskDrive.DeviceID='{disk.DeviceID}'}} WHERE AssocClass = Win32_DiskDriveToDiskPartition");
            ManagementObjectSearcher partitionSearcher = new ManagementObjectSearcher(scope, partitionQuery);

            foreach (ManagementObject partition in partitionSearcher.Get())
            {
                Partition newPartition = new Partition();
                newPartition.Caption = partition["Caption"].ToString();
                newPartition.DeviceID = partition["DeviceID"].ToString();
                newPartition.Size = long.Parse(partition["Size"].ToString());

                 newPartition.LogicalDisks = new ObservableCollection<LogicalDisk>(GetLogicalDisks(newPartition, scope));
                yield return newPartition;
            }
        }


        public static ObservableCollection<Disk> GetDisks()
        {
            ObservableCollection<Disk> disks = new ObservableCollection<Disk>();
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

                
                newDisk.Partitions = new ObservableCollection<Partition>(GetPartitions(newDisk, scope));

                disks.Add(newDisk);
            }
            return disks;
        }
    }
}
