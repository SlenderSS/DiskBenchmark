using DiskBenchmark.Infrastructure.Common;
using DiskBenchmark.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using StorageSpeedMeter;
using System.Windows;

namespace DiskBenchmark.Services
{
    internal class DisksService
    {
        public static readonly long FILE_SIZE = 1024 * 1024 * 1024;
        



        public SmartDisk GetSmartInformation(Disk disk)
        {
            SmartDisk smartDisk = new SmartDisk();
            try
            {
                ManagementObjectSearcher mangObjsearc = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive WHERE Caption='" + disk.Caption + "'");

                foreach (ManagementObject manObj in mangObjsearc.Get())
                {
                    smartDisk.Type = manObj["MediaType"].ToString();
                    smartDisk.Model = manObj["Model"].ToString();
                    smartDisk.Serial = manObj["SerialNumber"].ToString();
                    smartDisk.InfrastructureType = manObj["InterfaceType"].ToString();
                    smartDisk.Capasity = ulong.Parse(manObj["Size"].ToString()); 
                    smartDisk.Partitions = Convert.ToUInt16(manObj["Partitions"].ToString());
                    smartDisk.Signature = manObj["Signature"] != null ? manObj["Signature"].ToString(): "None information" ;


                    smartDisk.FirmwareRevision = manObj["FirmwareRevision"] != null ?  manObj["FirmwareRevision"].ToString() : "Missing info";         
                    smartDisk.Sectors = Convert.ToUInt32(manObj["TotalSectors"].ToString());
                }


                #region Overall Smart Status

                try
                {
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
                       
                              throw new Exception(ex.Message);
                            }
                        }
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    MessageBox.Show("SMART data exception" + Environment.NewLine + ex.Message);
                    MessageBox.Show(ex.Message);
                    smartDisk.IsOK = false;
                }

                smartDisk.IsSupported = smartDisk.SmartAttributes.Where(sa => sa.HasData).Any();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Disk details exception" + Environment.NewLine + ex.Message);
            }
            smartDisk.SmartAttributes = new SmartAttributeCollection(smartDisk.SmartAttributes.Where(x => x.Current != 0 && x.Worst != 0).ToList());
            return smartDisk;
        }

        public ObservableCollection<Disk> GetDisks()
        {
            var disks = new ObservableCollection<Disk>();

            try
            {
                ManagementScope scope = new ManagementScope("\\\\.\\root\\CIMV2");
                ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_DiskDrive");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                foreach (ManagementObject disk in searcher.Get())
                {
                    Disk newDisk = new Disk();
                    newDisk.Caption = disk["Model"] != null ? disk["Model"].ToString() : "None";
                    newDisk.DeviceID = disk["DeviceID"] != null ? disk["DeviceID"].ToString() : "None";
                    newDisk.SerialNumber = disk["SerialNumber"] != null ? disk["SerialNumber"].ToString() : "None";
                    newDisk.Size = disk["Size"] != null ? ulong.Parse(disk["Size"].ToString()) : 0;
                    newDisk.PnpDeviceID = disk["PNPDeviceID"] != null ? disk["PNPDeviceID"].ToString() : "None";

                    ObjectQuery partitionQuery = new ObjectQuery($"ASSOCIATORS OF {{Win32_DiskDrive.DeviceID='{newDisk.DeviceID}'}} WHERE AssocClass = Win32_DiskDriveToDiskPartition");
                    ManagementObjectSearcher partitionSearcher = new ManagementObjectSearcher(scope, partitionQuery);

                    foreach (ManagementObject partition in partitionSearcher.Get())
                    {

                        ObjectQuery logicalDiskQuery = new ObjectQuery($"ASSOCIATORS OF {{Win32_DiskPartition.DeviceID='{partition["DeviceID"]}'}} WHERE AssocClass = Win32_LogicalDiskToPartition");
                        ManagementObjectSearcher logicalDiskSearcher = new ManagementObjectSearcher(scope, logicalDiskQuery);

                        foreach (ManagementObject logicalDisk in logicalDiskSearcher.Get())
                        {
                            LogicalDisk newLogicalDisk = new LogicalDisk();
                            newLogicalDisk.Caption = logicalDisk["Caption"] != null ? logicalDisk["Caption"].ToString() : "None";
                            newLogicalDisk.DeviceID = logicalDisk["DeviceID"] != null ? logicalDisk["DeviceID"].ToString() : "None";
                            newLogicalDisk.FileSystem = logicalDisk["FileSystem"] != null ? logicalDisk["FileSystem"].ToString() : "None";
                            newLogicalDisk.Size = logicalDisk["Size"] != null ? ulong.Parse(logicalDisk["Size"].ToString()) : 0;
                            newLogicalDisk.UsedSpace = newLogicalDisk.Size - ulong.Parse(logicalDisk["FreeSpace"].ToString());
                            newDisk.TotalUsedSpace += newLogicalDisk.UsedSpace;
                            newDisk.LogicalDisks.Add(newLogicalDisk);

                        }
                    }
                    disks.Add(newDisk);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Disks List exception" + Environment.NewLine + ex.Message);
            }
                //IEnumerable<Disk> disks = new IEnumerable<Disk>();
           
            return disks;
            
        }
    }
}
