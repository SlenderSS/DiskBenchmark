using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiskBenchmark.Models
{
    internal class SmartAttribute
    {
        public int Register { get; set; }
        public string Name { get; set; }

        public int Current { get; set; }
        public int Worst { get; set; }
        public int Threshold { get; set; }
        public int Data { get; set; }
        public bool IsOK { get; set; }

        public bool HasData
        {
            get
            {
                if (Current == 0 && Worst == 0 && Threshold == 0 && Data == 0)
                    return false;
                return true;
            }
        }

        public SmartAttribute(int register, string attributeName)
        {
            this.Register = register;
            this.Name = attributeName;
        }
    }
}
