using Coboss.Persistance.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Persistance.Entities
{
    public class Employee : BaseEntitiy
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public User User { get; set; }
    }
}
