using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using PrototypeProject.Models;
using System.Reflection.Metadata;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel;
using Document = MigraDoc.DocumentObjectModel.Document;
using MigraDoc.DocumentObjectModel.Tables;

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

        /*public IActionResult OnGet(string pdf)
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

                var reader = command.ExecuteReader();

                Stock Row = new List<Stock>();
                while (reader.Read())
                {
                    Row.Id = reader.GetInt32(0); //getting the first field from the table
                    Row.Title = reader.GetString(1); //getting the second field from the table
                    Row.Count = reader.GetInt32(2); //getting the third field from the table
                    Row.Price = reader.GetString(3); // We dont get the password. The role field is in the 5th position
                    Stock.Add(Row);
                }

            }

            //PDF code here!
            if (pdf == "1")
            {
                //Create an object for pdf document
                Document doc = new Document();
                Section sec = doc.AddSection();
                Paragraph para = sec.AddParagraph();

                para.Format.Font.Name = "Arial";
                para.Format.Font.Size = 14;
                para.Format.Font.Color = Color.FromCmyk(0, 0, 0, 100); //black colour
                para.AddFormattedText("List of Users", TextFormat.Bold);
                para.Format.SpaceAfter = "1.0cm";



                //Table
                Table tab = new Table();
                tab.Borders.Width = 0.75;
                tab.TopPadding = 5;
                tab.BottomPadding = 5;

                //Column
                Column col = tab.AddColumn(Unit.FromCentimeter(1.5));
                col.Format.Alignment = ParagraphAlignment.Justify;
                tab.AddColumn(Unit.FromCentimeter(3));
                tab.AddColumn(Unit.FromCentimeter(3));
                tab.AddColumn(Unit.FromCentimeter(2));

                //Row
                Row row = tab.AddRow();
                row.Shading.Color = Colors.Coral;

                //Cell for header
                Cell cell = new Cell();
                cell = row.Cells[0];
                cell.AddParagraph("ID");
                cell = row.Cells[1];
                cell.AddParagraph("Admin ID");
                cell = row.Cells[2];
                cell.AddParagraph("Admin Name");
                cell = row.Cells[3];
                cell.AddParagraph("Email");


                //Add data to table 
                for (int i = 0; i < Stock.Count; i++)
                {
                    row = tab.AddRow();
                    cell = row.Cells[0];
                    cell.AddParagraph(Convert.ToString(i + 1));
                    cell = row.Cells[1];
                    cell.AddParagraph(Stock[i].Title);
                    cell = row.Cells[2];
                    cell.AddParagraph(Stock[i].Price);
                    cell = row.Cells[3];
                    cell.AddParagraph(Stock[i].Count);
                }

                tab.SetEdge(0, 0, 4, (Stock.Count + 1), Edge.Box, BorderStyle.Single, 1.5, Colors.Black);
                sec.Add(tab);

                //Rendering
                PdfDocumentRenderer pdfRen = new PdfDocumentRenderer();
                pdfRen.Document = doc;
                pdfRen.RenderDocument();

                //Create a memory stream
                MemoryStream stream = new MemoryStream();
                pdfRen.PdfDocument.Save(stream); //saving the file into the stream

                Response.Headers.Add("content-disposition", new[] { "inline; filename = ListofAdmin.pdf" });
                return File(stream, "application/pdf");

            }

            return Page();
        }*/
    }
}
