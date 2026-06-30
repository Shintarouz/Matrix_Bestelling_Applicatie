using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts();


        public void AddProduct(Product product);

        public void UpdateProduct(Product product);

        public void DeleteProduct(Product product);

        Product GetProductById(int id);

        List<Product> SearchProducts(string searchedProduct);

        List<Product> GetProductsLowToHigh();

        List<Product> GetProductsHighToLow();

        List<Product> FilterPrice(decimal min, decimal? max);

        List<Product> FilterCategory(int? categoryId);
    }
}
