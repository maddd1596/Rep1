using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppEvraz.Entities
{
    public abstract class Transport : INotifyPropertyChanged
    {

            public Transport(int id)
            {
                Id = id;
            }

        public int Id {
            get { return id; }
            set { id = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("id")); }
        }
        private int id { get; set; }
        public string Name
        {
            get { return name; }
            set { name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("name")); }
        }
        private string name {get; set;}
        public string TransportType {
            get { return transportType; }
            set { transportType = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("transportType")); }
        }
        private string transportType { get; set; }
        public uint Speed
        {
            get { return speed; }
            set { speed = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("speed")); }
        }
        private uint speed { get; set; }
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("wheelPunctureProbabilityPercent"));
                }
            }
        }

        private int wheelPunctureProbabilityPercent { get; set; }

        public string AdditionalInfo {
            get { return additionalInfo; }
            set { additionalInfo = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("additionalInfo")); }
        }
        private string additionalInfo { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
