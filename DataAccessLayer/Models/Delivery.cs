using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DataAccessLayer.Models
{
    public class Delivery
    {
        public int Id { get; set; }

        public DateTime DeliveryDate { get; set; }
        public bool IsCompleted { get; set; } = false;

        public List<Order> Orders { get; set; } = new();

    }
}
