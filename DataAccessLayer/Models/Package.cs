using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer.Models
{
    public class Package
    {
        public int Id { get; set; }
        public string Barcode { get; set; } 
        public int PackageNmbr { get; set; }
        public int OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; } = null!;
    }
}
