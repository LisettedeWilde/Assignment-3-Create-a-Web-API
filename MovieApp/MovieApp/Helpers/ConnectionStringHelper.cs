using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace MovieApp.Helpers
{
  
        public class ConnectionStringHelper
        {
            public static string GetConnectionString()
            {
                SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
                sqlConnectionStringBuilder.DataSource = "MURAT-LAPTOP\\SQLEXPRESS";
                sqlConnectionStringBuilder.InitialCatalog = "MarvelUniverseDb";
                sqlConnectionStringBuilder.IntegratedSecurity = true;
                sqlConnectionStringBuilder.TrustServerCertificate = true;

                return sqlConnectionStringBuilder.ConnectionString;
            }

        }
}
