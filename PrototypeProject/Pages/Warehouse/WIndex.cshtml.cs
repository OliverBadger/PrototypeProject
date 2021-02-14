using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PrototypeProject.Pages.Warehouse
{
    public class WIndexModel : PageModel
    {
        public string Name;
        public const string SessionKeyName1 = "Name";

        public string SessionID;
        public const string SessionKeyName2 = "sessionID";


        public IActionResult OnGet()
        {
            Name = HttpContext.Session.GetString(SessionKeyName1);
            SessionID = HttpContext.Session.GetString(SessionKeyName2);

            if (string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(SessionID))
            {
                return RedirectToPage("/Login/Login");
            }
            return Page();
        }
    }
}
