using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class ProductModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        public Product Product { get; set; }
        public ProductModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult OnGet(int id)
        {
            Product = _productRepository.GetProductById(id);

            if (Product == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}


