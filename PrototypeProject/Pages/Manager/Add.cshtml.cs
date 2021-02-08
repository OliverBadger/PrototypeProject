using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrototypeProject.Models;

namespace PrototypeProject.Pages.Manager
{
    public class AddModel : PageModel
    {
        [BindProperty]
        public Stock Stock { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            DatabaseConnection dbstring = new DatabaseConnection(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"INSERT INTO Stock (Title, Count, Price) VALUES (@Title, @Count, @Price)";

                command.Parameters.AddWithValue("@Title", Stock.Title);
                command.Parameters.AddWithValue("@Count", Stock.Count);
                command.Parameters.AddWithValue("@Price", Stock.Price);

                Console.WriteLine(Stock.Title);
                Console.WriteLine(Stock.Count);
                Console.WriteLine(Stock.Price);

                command.ExecuteNonQuery();
            }
            return RedirectToPage("/Manager/View");
        }
    }
}
