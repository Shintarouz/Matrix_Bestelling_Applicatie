using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication35.Models;

namespace DataAccessLayer.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(100)]
        public required string Title { get; set; }
        public required string Comment { get; set; }


        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}