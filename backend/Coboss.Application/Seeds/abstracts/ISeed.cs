using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Application.Seeds.abstracts
{
    public interface ISeed
    {
        Task Seed();
    }
}
