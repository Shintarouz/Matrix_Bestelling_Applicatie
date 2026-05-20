using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Services;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class SearchModel : PageModel
    {
        private readonly CartService _cartService;
        public SearchModel(CartService cartService)
        {
            _cartService = cartService;
        }
        public int CartCount => _cartService.Items.Count;

        public void OnGet()
        {

        }
    }
}
