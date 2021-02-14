using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrototypeProject.Models;

namespace PrototypeProject.Pages.Manager.Messages
{
    public class MessagesDeleteModel : PageModel
    {
        [BindProperty]
        public MessageModel Messages { get; set; }
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
                command.CommandText = "SELECT * FROM Message WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", id);

                SqlDataReader reader = command.ExecuteReader();
                Messages = new MessageModel();
                while (reader.Read())
                {
                    Messages.Id = reader.GetInt32(0);
                    Messages.FirstName = reader.GetString(1);
                    Messages.LastName = reader.GetString(2);
                    Messages.Department = reader.GetString(3);
                    Messages.Message = reader.GetString(4);
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
                command.CommandText = "DELETE Message WHERE Id = @ID";
                command.Parameters.AddWithValue("@ID", Messages.Id);
                command.ExecuteNonQuery();
            }
            conn.Close();
            return RedirectToPage("/Warehouse/Messages/WMessages");
        }
    }
}
