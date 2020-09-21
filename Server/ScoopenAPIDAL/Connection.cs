using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace ScoopenAPIDAL
{
    public class Connection
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["ScoopenDB"].ConnectionString;
            }
        }

        public static SqlConnection SqlConnectionObject
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }
    }
}
