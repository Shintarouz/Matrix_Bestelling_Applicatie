using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using KE03_INTDEV_SE_1_Base.Helpers;
using System;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class SearchModel : PageModel
    {
        private readonly MatrixIncDbContext _context;

        public SearchModel(MatrixIncDbContext context)
        {
            _context = context;
        }
        public List<CartItem> CartItems { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public decimal CartTotal { get; set; }
        public decimal ShippingCost { get; set; } = 4.99m;
        public decimal VAT { get; set; }
        public decimal FinalTotal { get; set; }

        public int CartCount => CartItems.Sum(x => x.Quantity);

        public void OnGet()
        {
            CartItems = HttpContext.Session.GetObject<List<CartItem>>("cart") ?? new List<CartItem>();

            CartTotal = CartItems.Sum(x => x.Price * x.Quantity);
            VAT = CartTotal * 0.21m;
            FinalTotal = CartTotal + ShippingCost;
            Orders = _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .OrderByDescending(o => o.OrderDate)
                    .ToList();
        }

        public IActionResult OnPostAddToCart(int productId, string name, decimal price)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart")
               ?? new List<CartItem>();

            var existingItem = cart.FirstOrDefault(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
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

            return RedirectToPage(); // refresh page
        }

        public IActionResult OnPostRemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart")
                ?? new List<CartItem>();

            var item = cart.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                cart.Remove(item);
            }

            HttpContext.Session.SetObject("cart", cart);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostCheckout()
        {
            var cart = HttpContext.Session.GetObject<List<CartItem>>("cart")
                ?? new List<CartItem>();

            if (!cart.Any())
            {
                return RedirectToPage("/FinishOrder");
            }

            // Temporary customer id
            // Replace later with logged-in user id
            int customerId = 1;
    //        var delivery = await _context.Deliveries
    //.FirstOrDefaultAsync(d => d.Id == deliveryId);

    //        if (delivery == null)
    //        {
    //            return Content($"Delivery {deliveryId} does not exist");
    //        }
            var order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerId = customerId,
           //     DeliveryId = delivery.Id
            };

            foreach (var item in cart)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,

                });
            }

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            // Clear cart
            HttpContext.Session.Remove("cart");

            return RedirectToPage("/FinishOrder");
        }
    }
}
