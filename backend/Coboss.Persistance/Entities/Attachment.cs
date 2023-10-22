using Coboss.Persistance.Entities.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Persistance.Entities
{
    public class Attachment : BaseEntitiy
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
