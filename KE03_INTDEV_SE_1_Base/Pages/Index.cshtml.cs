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

        // voor nieuwe producten aan te maken in de database
        //public IActionResult OnPostAddProduct()
        //{
        //    var product = new Product
        //    {
        //        Name = "New Product",
        //        Description = "This is a new product",
        //        Price = 19.99m
        //    };
        //    _productRepository.AddProduct(product);
        //    return RedirectToPage();
        //}

        //public IActionResult OnPostAddTestItem()
        //{
        //    _cartService.Items.Add(new CartItem
        //    {
        //        Id = 1,
        //        Name = "Test Product",
        //        Price = 10,
        //        Quantity = 1
        //    });

        //    return RedirectToPage();
        //}
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
        public IActionResult OnPostAddToCart(int productId, string name, decimal price)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart")
                       ?? new List<CartItem>();

            var existing = cart.FirstOrDefault(x => x.ProductId == productId);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = productId,
                    Name = name,
                    Price = price,
                    Quantity = 1

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
