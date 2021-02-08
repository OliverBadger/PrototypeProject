using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrototypeProject.Models
{
    public class LoginModelFile
    {
        [BindProperty]
        public string Department { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
