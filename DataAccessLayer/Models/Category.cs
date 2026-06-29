using System.ComponentModel.DataAnnotations;

namespace WebApplication35.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? SubCategory { get; set; }

    }
}