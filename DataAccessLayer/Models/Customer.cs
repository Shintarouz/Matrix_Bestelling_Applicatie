using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Customer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Description may only contain letters.")]
        public required string userName { get; set; }

        [Required]
        [RegularExpression(@"^\d{4}\s?[A-Za-z]{2}$", ErrorMessage = "Address must be in Dutch format (e.g. 1234 AB).")]
        public string? Address { get; set; }

        public bool Active { get; set; }
        public string? Password { get; set; }

        public string? Email { get; set; }

        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}