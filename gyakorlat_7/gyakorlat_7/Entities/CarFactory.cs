using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gyakorlat_7.Abstractions;

namespace gyakorlat_7.Entities
{
    class CarFactory : IToyFactory
    {

        Toy IToyFactory.CreateNew()
        {
            return new Car();
        }
    }
}
