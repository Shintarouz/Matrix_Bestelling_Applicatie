namespace WebApplication35.Models
{
    public class Complain
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool NewComplain { get; set; } = true;
        public string? Description { get; set; }
    }
}
