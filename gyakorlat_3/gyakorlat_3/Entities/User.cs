using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gyakorlat_3.Entities
{
    class User
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fullname 
        { get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }    
        }


    }
}
