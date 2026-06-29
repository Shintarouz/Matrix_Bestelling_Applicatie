using DataAccessLayer.Models;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

public class OrderDetailModel : PageModel
{
    private readonly MatrixIncDbContext _context;

    public OrderDetailModel(MatrixIncDbContext context)
    {
        _context = context;
    }

    public Order Order { get; set; } = null!;

    public decimal OrderTotal =>
        Order.OrderItems.Sum(i => i.Price * i.Quantity);

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Order = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (Order == null)
        {
            return NotFound();
        }

        return Page();
    }
}