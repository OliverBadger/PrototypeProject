using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrototypeProject.Models
{
    public class Stock
    {
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Price { get; set; }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public int Count { get; set; }

        public static implicit operator Stock(List<Stock> v)
        {
            throw new NotImplementedException();
        }
    }
}
