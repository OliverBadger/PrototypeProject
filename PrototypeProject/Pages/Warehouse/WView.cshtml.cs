using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrototypeProject.Models;

namespace PrototypeProject.Pages.Warehouse
{
    public class WViewModel : PageModel
    {
        public List<Stock> Stock { get; set; }

        public void OnGet()
        {
            DatabaseConnection dbstring = new DatabaseConnection(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT * FROM Stock";

                SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

                Stock = new List<Stock>(); //this object of list is created to populate all records from the table

                while (reader.Read())
                {
                    Stock record = new Stock
                    {
                        Id = reader.GetInt32(0), //getting the first field from the table
                        Title = reader.GetString(1), //getting the second field from the table
                        Count = reader.GetInt32(2), //getting the third field from the table
                        Price = reader.GetString(3)
                    }; //a local var to hold a record temporarily

                    Stock.Add(record); //adding the single record into the list
                }

                // Call Close when done reading.
                reader.Close();
            }
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("/Warehouse/WAdd");
        }
    }
}
