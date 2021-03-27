using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppEvraz.Entities
{
    abstract class Transport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public uint Speed { get; set; }
        public int WheelPunctureProbabilityPercent { 
            get
            {
                return wheelPunctureProbabilityPercent;
            }

            set
            {
                if(value >= 0 && value <= 100)
                {
                    wheelPunctureProbabilityPercent = value;
                }
            }
        }

        private int wheelPunctureProbabilityPercent { get; set; }

        public void StartCircle()
        {

        }
    }
}
