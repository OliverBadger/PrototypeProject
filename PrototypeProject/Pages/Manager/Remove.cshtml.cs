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
    public class RemoveModel : PageModel
    {
        [BindProperty]
        public Stock Stock{ get; set; }
        public IActionResult OnGet(int? id)
        {
            DatabaseConnection dbstring = new DatabaseConnection(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "SELECT * FROM Stock WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();
                Stock = new Stock();
                while (reader.Read())
                {
                    Stock.Id = reader.GetInt32(0);
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
            DatabaseConnection dbstring = new DatabaseConnection(); //creating an object from the class
            string DbConnection = dbstring.DatabaseString(); //calling the method from the class
            Console.WriteLine(DbConnection);
            SqlConnection conn = new SqlConnection(DbConnection);
            conn.Open();

            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = "DELETE Stock WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", Stock.Id);
                command.ExecuteNonQuery();
            }
            conn.Close();
            return RedirectToPage("/Manager/View");
        }
    }
}
