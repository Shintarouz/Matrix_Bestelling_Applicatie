using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication35.Models;

namespace DataAccessLayer.Models
{
    public class Return
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool NewReturn { get; set; } = true;
        public List<Product> ReturnProducts { get; set; } = new List<Product>();
    }
}
