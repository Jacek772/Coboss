using Coboss.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Persistance
{
    public class DatabaseConfiguration
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Schema { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }


        public string ConnectionString
            => $"Server={Server};Port={Port};Database={Database};User Id={UserId}; Password={Password};";
    }
}
