using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Persistence
{
    /// <summary>
    /// The class connection
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private static string connectionString = null;

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <returns></returns>
        public static Apps4KidsEntities GetContext()
        {
            Apps4KidsEntities result;

            if (connectionString != null)
            {
                return new Apps4KidsEntities(connectionString);
            }

            EntityConnectionStringBuilder ecsb = new EntityConnectionStringBuilder();

            SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder();

            ecsb.Metadata = @"res://*/DataModel.csdl|
                            res://*/DataModel.ssdl|
                            res://*/DataModel.msl";

            ecsb.Provider = "System.Data.SqlClient";

            sqlcsb.InitialCatalog = "Apps4Kids";
            sqlcsb.DataSource = "localhost";
            sqlcsb.UserID = "sa";
            sqlcsb.Password = "123user!";

            ecsb.ProviderConnectionString = sqlcsb.ToString();

            result = new Apps4KidsEntities(ecsb.ConnectionString);
            return result;
        }
    }
}
