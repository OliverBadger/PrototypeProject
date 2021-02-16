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
    public class SentModel : PageModel
	{
		public List<MessageModel> Message { get; set; }
		public MessageModel Department { get; set; }

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
				command.CommandText = @"SELECT * FROM Message";

				SqlDataReader reader = command.ExecuteReader(); //SqlDataReader is used to read record from a table

				Message = new List<MessageModel>(); //this object of list is created to populate all records from the table
				Department = new MessageModel();

				while (reader.Read())
				{
					Department.Department = reader.GetString(3);
					if (Department.Department == "Warehouse")
					{
						MessageModel record = new MessageModel
						{
							Id = reader.GetInt32(0), //getting the first field from the table
							FirstName = reader.GetString(1), //getting the first field from the table
							LastName = reader.GetString(2), //getting the second field from the table
							Message = reader.GetString(4), //getting the third field from the table
						}; //a local var to hold a record temporarily
						Message.Add(record); //adding the single record into the list
					}
				}
				reader.Close();
			}
		}
		public IActionResult OnPost()
		{
			return RedirectToPage("/Warehouse/Messages/WMessagesAdd");
		}
	}
}
