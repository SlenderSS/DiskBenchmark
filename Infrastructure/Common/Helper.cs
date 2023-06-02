using DiskBenchmark.Models;
using System;

namespace DiskBenchmark.Infrastructure.Common
{
    internal sealed class Helper
    {
        public static int ConvertStringHexToInt(string hex0x0)
        {
            try
            {
                int value = (int)new System.ComponentModel.Int32Converter().ConvertFromString(hex0x0);
                return value;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error converting hex value {hex0x0} to integer.", ex);
            }
        }
        public static SmartAttributeCollection GetSmartRegisters(string textRegisters)
        {
            var collection = new SmartAttributeCollection();

            try
            {
                var splitOnCRLF = textRegisters.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in splitOnCRLF)
                {
                    var splitLineOnComma = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    string register = splitLineOnComma[0].Trim();
                    string attributeName = splitLineOnComma[1].Trim();

                    collection.Add(new SmartAttribute(Helper.ConvertStringHexToInt(register), attributeName));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetSmartRegisters failed with error " + ex);
            }

            return collection;
        }
    }
}
