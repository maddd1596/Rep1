using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppEvraz.Entities
{
    class Truck : Transport
    {
        public Truck(int id) : base(id)
        {

        }

        public double CargoWeight
        {
            get
            {
                return cargoWeight;
            }
            set
            {
                if (value >= 0)
                {
                    cargoWeight = value;
                }
            }
        }
        private double cargoWeight { get; set; }

    }
}
