using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppEvraz.Entities
{
    class Car : Transport
    {

        public Car(int id) : base(id)
        {

        }
        public int PeopleInsideCount { 
            get
            {
                return peopleInsideCount;
            }
            set
            {
                if (value >= 0)
                {
                    peopleInsideCount = value;
                }
            }
        }
        private int peopleInsideCount { get; set; }
    }
}
