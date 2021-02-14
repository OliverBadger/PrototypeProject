using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PrototypeProject.Models;

namespace PrototypeProject.Pages.Warehouse.Messages
{
    public class WMessagesAddModel : PageModel
    {
            [BindProperty]
            public MessageModel Message{ get; set; }
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
                    command.CommandText = @"INSERT INTO Message(FirstName, LastName, Department, Message) VALUES (@FName, @LName, @Department, @Message)";

                    command.Parameters.AddWithValue("@FName", Message.FirstName);
                    command.Parameters.AddWithValue("@LName", Message.LastName);
                    command.Parameters.AddWithValue("@Department", "Manager");
                    command.Parameters.AddWithValue("@Message", Message.Message);

                    Console.WriteLine(Message.FirstName);
                    Console.WriteLine(Message.LastName);
                    Console.WriteLine(Message.Message);

                    command.ExecuteNonQuery();
                }
                return RedirectToPage("/Warehouse/Messages/Messages");
            }
        }
    }
