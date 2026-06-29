using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using KE03_INTDEV_SE_1_Base.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        //private readonly IPartRepository _partRepository;

        public List<CartItem> CartItems { get; set; } = new();
        public decimal CartTotal { get; set; }
        public int CartCount { get; set; }
        public IList<Customer> Customers { get; set; }
        public IList<Product> Products { get; set; }
        //public IList<Part> Parts { get; set; }
        public IndexModel(
            ILogger<IndexModel> logger,
            ICustomerRepository customerRepository,
            IProductRepository productRepository)
            //IPartRepository partRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            //_partRepository = partRepository;


            Customers = new List<Customer>();
            Products = new List<Product>();
            //Parts = new List<Part>();


        }
        public IActionResult OnPostUpdateQuantity(int productId, int quantity)
        {
            LoadCart();

            var item = CartItems.FirstOrDefault(i => i.ProductId == productId);

            if (item != null)
            {
                if (quantity <= 0)
                {
                    CartItems.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
            }

            SaveCart();

            return RedirectToPage();
        }

        private void SaveCart()
        {
            HttpContext.Session.SetObject("cart", CartItems);
            CartTotal = CartItems.Sum(x => x.Price * x.Quantity);
        }

        public void OnGet()
        {            
            Customers = _customerRepository.GetAllCustomers().ToList();        
            Products = _productRepository.GetAllProducts().ToList();
            //Parts = _partRepository.GetAllParts().ToList();

            CartItems = HttpContext.Session.GetObject<List<CartItem>>("cart")?? new List<CartItem>();
            CartCount = CartItems.Sum(x => x.Quantity);
            CartTotal = CartItems.Sum(x => x.Price * x.Quantity);

            LoadCart();

            _logger.LogInformation($"Customers: {Customers.Count}, Products: {Products.Count}");

        }
        public IActionResult OnPostAddToCart(int productId, string name, decimal price, int quantity)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart")
                       ?? new List<CartItem>();

            var existing = cart.FirstOrDefault(x => x.ProductId == productId);

            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    Name = name,
                    Price = price,
                    Quantity = quantity

                });
            }

            HttpContext.Session.SetObject("cart", cart);

            return RedirectToPage();
        }

        public IActionResult OnPostRemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart")
                       ?? new List<CartItem>();

            var item = cart.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.SetObject("cart", cart);
            }

            return RedirectToPage();
        }

        private void LoadCart()
        {
            CartItems = HttpContext.Session.GetObject<List<CartItem>>("cart")
                        ?? new List<CartItem>();

            CartTotal = CartItems.Sum(x => x.Price * x.Quantity);
        }
    }
}
