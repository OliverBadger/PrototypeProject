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
            string DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Oliver\Desktop\Uni Documents\Semester 2 Project\PrototypeProject\PrototypeProject\Data\MasterDB.mdf;Integrated Security=True";
            return DbString;

        }
    }
}
