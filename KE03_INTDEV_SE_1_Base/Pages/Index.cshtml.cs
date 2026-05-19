using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class IndexModel : PageModel
    {
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
            IPartRepository partRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _partRepository = partRepository;


            Customers = new List<Customer>();
            Products = new List<Product>();
            Parts = new List<Part>();
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
