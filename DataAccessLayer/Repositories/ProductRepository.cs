using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MatrixIncDbContext _context;

        public ProductRepository(MatrixIncDbContext context) 
        {
            _context = context;
        }
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products;
            //return _context.Products.Include(p => p.Parts);
        }

        public Product? GetProductById(int id)
        {
            //return _context.Products.Include(p => p.Parts).FirstOrDefault(p => p.Id == id);
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        //door timo gemaakt
        public List<Product> SearchProducts(string search)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p =>
                    p.Name.Contains(search) ||
                    p.Description.Contains(search) ||
                    p.Category.Name.Contains(search) ||
                    p.Price.ToString().Contains(search))
                .ToList();
        }
        //door timo gemaakt
        public List<Product> GetProductsLowToHigh()
        {
            return _context.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Price)
                .ToList();
        }
        //door timo gemaakt
        public List<Product> GetProductsHighToLow()
        {
            return _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Price)
                .ToList();
        }
        //door timo gemaakt
        public List<Product> FilterCategory(int? categoryId)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => !categoryId.HasValue || p.CategoryId == categoryId)
                .ToList();
        }
        //door timo gemaakt
        public List<Product> FilterPrice(decimal min, decimal? max)
        {
            return _context.Products
                .Include(p => p.Category)
                .Where(p => p.Price >= min && (!max.HasValue || p.Price <= max))
                .OrderByDescending(p => p.Price)
                .ToList();
        }
    }
}
