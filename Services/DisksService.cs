using DiskBenchmark.Infrastructure.Common;
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
    internal class DisksService
    {

        public SmartDisk GetSmartInformation(Disk disk)
        {
            SmartDisk smartDisk = new SmartDisk();
            try
            {
                #region Overall Smart Status

                ManagementScope scope = new ManagementScope("\\\\.\\ROOT\\WMI");
                ObjectQuery query = new ObjectQuery(@"SELECT * FROM MSStorageDriver_FailurePredictStatus Where InstanceName like ""%"
                                                    + disk.PnpDeviceID.Replace("\\", "\\\\") + @"%""");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                ManagementObjectCollection queryCollection = searcher.Get();
                foreach (ManagementObject m in queryCollection)
                {
                    smartDisk.IsOK = (bool)m.Properties["PredictFailure"].Value == false;
                }

                #endregion

                #region Smart Registers

                smartDisk.SmartAttributes.AddRange(Helper.GetSmartRegisters(Resource.SmartAttributes));

                searcher.Query = new ObjectQuery(@"Select * from MSStorageDriver_FailurePredictData Where InstanceName like ""%"
                                                     + disk.PnpDeviceID.Replace("\\", "\\\\") + @"%""");

                foreach (ManagementObject data in searcher.Get())
                {
                    byte[] bytes = (byte[])data.Properties["VendorSpecific"].Value;
                    for (int i = 0; i < 42; ++i)
                    {
                        try
                        {
                            int id = bytes[i * 12 + 2];

                            int flags = bytes[i * 12 + 4]; // least significant status byte, +3 most significant byte, but not used so ignored.
                                                           //bool advisory = (flags & 0x1) == 0x0;
                            bool failureImminent = (flags & 0x1) == 0x1;
                            //bool onlineDataCollection = (flags & 0x2) == 0x2;

                            int value = bytes[i * 12 + 5];
                            int worst = bytes[i * 12 + 6];
                            int vendordata = BitConverter.ToInt32(bytes, i * 12 + 7);
                            if (id == 0) continue;

                            var attr = smartDisk.SmartAttributes.GetAttribute(id);
                            if (attr != null)
                            {
                                attr.Current = value;
                                attr.Worst = worst;
                                attr.Data = vendordata;
                                attr.IsOK = failureImminent == false;
                            }
                        }
                        catch (Exception ex)
                        {
                        // given key does not exist in attribute collection (attribute not in the dictionary of attributes)
                        throw new Exception(ex.Message);
                        }
                    }
                }

                    searcher.Query = new ObjectQuery(@"Select * from MSStorageDriver_FailurePredictThresholds Where InstanceName like ""%"
                                                     + disk.PnpDeviceID.Replace("\\", "\\\\") + @"%""");
                foreach (ManagementObject data in searcher.Get())
                {
                    byte[] bytes = (byte[])data.Properties["VendorSpecific"].Value;
                    for (int i = 0; i < 42; ++i)
                    {
                        try
                        {
                            int id = bytes[i * 12 + 2];
                            int thresh = bytes[i * 12 + 3];
                            if (id == 0) continue;

                            var attr = smartDisk.SmartAttributes.GetAttribute(id);
                            if (attr != null)
                            {
                                attr.Threshold = thresh;

                             
                            }
                        }
                        catch (Exception ex)
                        {
                        // given key does not exist in attribute collection (attribute not in the dictionary of attributes)
                        throw new Exception(ex.Message);
                        }
                    }
                }

                #endregion

                smartDisk.IsSupported = smartDisk.SmartAttributes.Where(sa => sa.HasData).Any();
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving Smart data for one or more drives. " + ex.Message);
            }

            return smartDisk;
        }



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
                newDisk.PnpDeviceID = disk["PNPDeviceID"].ToString();

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
