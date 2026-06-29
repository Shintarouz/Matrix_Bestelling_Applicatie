using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class AccountModel : PageModel
{
    private readonly MatrixIncDbContext _context;

    public List<Order> Orders { get; set; } = new();

    public AccountModel(MatrixIncDbContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        Orders = _context.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .OrderByDescending(o => o.OrderDate)
            .ToList();
    }
}