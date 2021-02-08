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
    public class EditModel : PageModel
    {
        [BindProperty]
        public Stock Stock { get; set; }

        public IActionResult OnGet(int? id)
        {
            DatabaseConnection dbstring = new DatabaseConnection(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();



            Stock = new Stock();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Stock";

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Stock.Title = reader.GetString(1);
                    Stock.Count = reader.GetInt32(2);
                    Stock.Price = reader.GetString(3);
                }


            }

            conn.Close();

            return Page();

        }


        public IActionResult OnPost()
        {
            DatabaseConnection dbstring = new DatabaseConnection();
            string DbConnection = dbstring.DatabaseString();
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            Console.WriteLine("Title: " + Stock.Title);
            Console.WriteLine("Amount of Stock: " + Stock.Count);
            Console.WriteLine("Stock Price: " + Stock.Price);

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "UPDATE Stock SET Title = @Title, Count = @Count, Price= @Price";

                command.Parameters.AddWithValue("@Title", Stock.Title);
                command.Parameters.AddWithValue("@Count", Stock.Count);
                command.Parameters.AddWithValue("@Price", Stock.Price);

                command.ExecuteNonQuery();
            }

            conn.Close();

            return RedirectToPage("/Manager/View");
        }
    }
}
