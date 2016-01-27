using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Persistence
{
    public class ProducerID
    {
        public static int Get(string name)
        {
            using (var context = Connection.GetContext())
            {
                ObjectParameter parameter = new ObjectParameter("ID", typeof(int));

                context.sp_GetProducerID(name, parameter);
                context.SaveChanges();
                return (int)parameter.Value;
            }


        }
    }
}
