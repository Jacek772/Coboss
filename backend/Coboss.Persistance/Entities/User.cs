using Coboss.Persistance.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Persistance.Entities
{
    public class User : BaseEntitiy
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }

        public Employee Employee { get; set; }
    }
}
