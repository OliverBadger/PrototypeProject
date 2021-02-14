using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using PrototypeProject.Models;

namespace PrototypeProject.Pages.Login
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginModelFile Login { get; set; }
        public string Message { get; set; }
        public string SessionId { get; set; }

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

            Console.WriteLine(Login.Department);
            Console.WriteLine(Login.Password);


            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = conn;
                command.CommandText = @"SELECT Department, Password, firstName, lastName FROM Login WHERE Department = @DPT AND Password = @PSWD";

                command.Parameters.AddWithValue("@DPT", Login.Department);
                command.Parameters.AddWithValue("@PSWD", Login.Password);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Login.Department = reader.GetString(0);
                    Login.firstName = reader.GetString(2);
                    Login.lastName = reader.GetString(3);
                }
            }
            if (!string.IsNullOrEmpty(Login.firstName))
            {
                SessionId = HttpContext.Session.Id;
                HttpContext.Session.SetString("sessionID", SessionId);
                HttpContext.Session.SetString("Name", Login.firstName);

                if (Login.Department == "Warehouse")
                {
                    return RedirectToPage("/Warehouse/WIndex");
                }
                else if (Login.Department == "Manager")
                {
                    return RedirectToPage("/Manager/View");
                }
                else if (Login.Department == "Admin")
                {
                    return RedirectToPage("/Admin/Index");
                }
            }
            else
            {
                Message = "Invalid Username and Password!";
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
