using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrototypeProject.Pages
{
    public class DatabaseConnection
    {
        public string DatabaseString()
        {
            string DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=MasterDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            return DbString;
        }
    }
}
