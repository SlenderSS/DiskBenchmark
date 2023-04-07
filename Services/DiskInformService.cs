using DiskBenchmark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DiskBenchmark.Services
{
    class DiskInformService
    {
        //public List<HDD> GetGetailsHDD()
        //{
        //    var dicDrives = new List<HDD>();
        //    try
        //    {       
        //        var wdSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
        //        int iDriveIndex = 0;
        //        foreach (ManagementObject drive in wdSearcher.Get())
        //        {
        //            var hdd = new HDD();
        //            hdd.Model = drive["Model"].ToString().Trim();
        //            hdd.Type = drive["InterfaceType"].ToString().Trim();
        //            hdd.Serial = drive["SerialNumber"] == null ? "None" : drive["SerialNumber"].ToString().Trim();
        //            dicDrives.Add(hdd);
        //            iDriveIndex++;
        //        }
        //        var pmsearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

        //        // get wmi access to hdd 
        //        var searcher = new ManagementObjectSearcher("Select * from Win32_DiskDrive");
        //        searcher.Scope = new ManagementScope(@"\root\wmi");

        //        //check if SMART reports the drive is failing
        //        searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictStatus");
        //        iDriveIndex = 0;
        //        foreach (ManagementObject drive in searcher.Get())
        //        {
        //            dicDrives[iDriveIndex].IsOK = (bool)drive.Properties["PredictFailure"].Value == false;
        //            iDriveIndex++;
        //        }

        //        // retrive attribute flags, value worste and vendor data information
        //        searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictData");
        //        iDriveIndex = 0;
        //        foreach (ManagementObject data in searcher.Get())
        //        {
        //            Byte[] bytes = (Byte[])data.Properties["VendorSpecific"].Value;
        //            for (int i = 0; i < 30; ++i)
        //            {
        //                try
        //                {
        //                    int id = bytes[i * 12 + 2];

        //                    int flags = bytes[i * 12 + 4]; // least significant status byte, +3 most significant byte, but not used so ignored.
        //                                                   //bool advisory = (flags & 0x1) == 0x0;
        //                    bool failureImminent = (flags & 0x1) == 0x1;
        //                    //bool onlineDataCollection = (flags & 0x2) == 0x2;

        //                    int value = bytes[i * 12 + 5];
        //                    int worst = bytes[i * 12 + 6];
        //                    int vendordata = BitConverter.ToInt32(bytes, i * 12 + 7);
        //                    if (id == 0) continue;

        //                    var attr = dicDrives[iDriveIndex].Attributes[id];
        //                    attr.Current = value;
        //                    attr.Worst = worst;
        //                    attr.Data = vendordata;
        //                    attr.IsOK = failureImminent == false;
        //                }
        //                catch
        //                {
        //                    // given key does not exist in attribute collection (attribute not in the dictionary of attributes)
        //                }
        //            }
        //            iDriveIndex++;
        //        }

        //        // retreive threshold values foreach attribute
        //        searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictThresholds");
        //        iDriveIndex = 0;
        //        foreach (ManagementObject data in searcher.Get())
        //        {
        //            Byte[] bytes = (Byte[])data.Properties["VendorSpecific"].Value;
        //            for (int i = 0; i < 30; ++i)
        //            {
        //                try
        //                {

        //                    int id = bytes[i * 12 + 2];
        //                    int thresh = bytes[i * 12 + 3];
        //                    if (id == 0) continue;

        //                    var attr = dicDrives[iDriveIndex].Attributes[id];
        //                    attr.Threshold = thresh;
        //                }
        //                catch
        //                {
        //                    // given key does not exist in attribute collection (attribute not in the dictionary of attributes)
        //                }
        //            }

        //            iDriveIndex++;
        //        }

        //    }
        //    catch(Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //    return dicDrives;
                // print
                //foreach (var drive in dicDrives)
                //{
                //    Console.WriteLine("-----------------------------------------------------");
                //    Console.WriteLine(" DRIVE ({0}): " + drive.Serial + " - " + drive.Model + " - " + drive.Type, ((drive.IsOK) ? "OK" : "BAD"));
                //    Console.WriteLine("-----------------------------------------------------");
                //    Console.WriteLine("");

                //    Console.WriteLine("ID                   Current  Worst  Threshold  Data  Status");
                //    foreach (var attr in drive.Attributes)
                //    {
                //        if (attr.Value.HasData)
                //            Console.WriteLine("{0}\t {1}\t {2}\t {3}\t " + attr.Value.Data + " " + ((attr.Value.IsOK) ? "OK" : ""), attr.Value.Attribute, attr.Value.Current, attr.Value.Worst, attr.Value.Threshold);
                //    }
                //    Console.WriteLine();
                //    Console.WriteLine();
                //    Console.WriteLine();
              
        
    }
}
