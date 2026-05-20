using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CartService _cartService;
        private readonly ILogger<IndexModel> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPartRepository _partRepository;

        public IList<Customer> Customers { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Part> Parts { get; set; }
        public IndexModel(
            ILogger<IndexModel> logger,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IPartRepository partRepository,
            CartService cartService)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _partRepository = partRepository;
            _cartService = cartService;


            Customers = new List<Customer>();
            Products = new List<Product>();
            Parts = new List<Part>();


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

        public int CartCount => _cartService.Items.Count;
        public IActionResult OnPostAddTestItem()
        {
            _cartService.Items.Add(new CartItem
            {
                Id = 1,
                Name = "Test Product",
                Price = 10,
                Quantity = 1
            });

            return RedirectToPage();
        }
        public void OnGet()
        {            
            Customers = _customerRepository.GetAllCustomers().ToList();        
            Products = _productRepository.GetAllProducts().ToList();
            Parts = _partRepository.GetAllParts().ToList();

            _logger.LogInformation($"Customers: {Customers.Count}, Products: {Products.Count}");

        }
    }
}
