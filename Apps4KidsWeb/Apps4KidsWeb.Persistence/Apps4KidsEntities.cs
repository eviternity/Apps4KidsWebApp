using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Persistence
{
    public partial class Apps4KidsEntities : DbContext
    {
        public Apps4KidsEntities(string connectionString):base(connectionString)
        {

        }
    }
}
