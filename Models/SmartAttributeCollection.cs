using System.Collections.Generic;

namespace DiskBenchmark.Models
{
    internal class SmartAttributeCollection : List<SmartAttribute>
    {
        public SmartAttributeCollection()
        {

        }

        public SmartAttribute GetAttribute(int registerID)
        {
            foreach (var item in this)
            {
                if (item.Register == registerID)
                    return item;
            }

            return null;
        }
    }
}
