using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAppEvraz.Entities;
using Newtonsoft.Json;
using System.IO;

namespace TestAppEvraz
{
    public class Config
    {
        public int CircleLength = 100;
        public List<Transport> TransportList = new List<Transport>();
        public double Truck_CargoWeightKgSpeedConsuming = 0.05;
        public double Car_OneManSpeedConsuming = 1;
        public double Motorcycle_CarriageSpeedConsuming = 10;
    }

    
}
