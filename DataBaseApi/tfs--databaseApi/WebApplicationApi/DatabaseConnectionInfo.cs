using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationApi
{
    public class DatabaseConnectionInfo
    {
        public string dataSource ;
        public string InitialCatalog;
        public string UserID;
        public string Password;

        public DatabaseConnectionInfo()
        {
            dataSource = "192.168.133.43";
            InitialCatalog = "cabimobila_studio";
            UserID = "sa";
            Password = "da711r!#%";
        }

        public string GetConnectionString()
        {
            string connectionString = "Data Source =" + dataSource +
                                "; Initial Catalog =" + InitialCatalog +
                                "; User ID=" + UserID +
                                "; Password=" + Password;
            return connectionString;
        }

    }
}
