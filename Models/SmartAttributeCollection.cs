using System.Collections.Generic;

namespace DiskBenchmark.Models
{
    internal class SmartAttributeCollection : List<SmartAttribute>
    {
        public SmartAttributeCollection()
        {

        }
        public SmartAttributeCollection(IEnumerable<SmartAttribute> smartAttributes) : base(smartAttributes)
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
