using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using KE03_INTDEV_SE_1_Base.Helpers;
using System;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class FinishOrderModel : PageModel
    {
        private readonly MatrixIncDbContext _context;

        public FinishOrderModel(MatrixIncDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnPostFinischOrderAsync()
        {
            return Content(
                "<script>alert('Your order has been placed successfully!'); window.location='/Index';</script>",
                "text/html");

        }
    }
}
