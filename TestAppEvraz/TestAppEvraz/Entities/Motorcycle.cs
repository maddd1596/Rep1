using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppEvraz.Entities
{
    class Motorcycle : Transport
    {
        public Motorcycle(int id) : base(id)
        {

        }
        public bool HaveCarriage { get; set; }
    }
}
