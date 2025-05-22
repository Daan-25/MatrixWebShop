using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class CartModel : PageModel
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        public decimal TotaalPrijs { get; set; }

        public void OnGet()
        {
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
            TotaalPrijs = CartItems.Sum(i => i.Part.Price * i.Aantal);
        }

        public IActionResult OnPostUpdateAantal(int id, string actie)
        {
            CartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();

            var item = CartItems.FirstOrDefault(i => i.Part.Id == id);
            if (item != null)
            {
                if (actie == "verhoog")
                {
                    item.Aantal++;
                }
                else if (actie == "verlaag")
                {
                    item.Aantal--;
                    if (item.Aantal <= 0)
                    {
                        CartItems.Remove(item);
                    }
                }
            }

            HttpContext.Session.SetObjectAsJson("cart", CartItems);

            return RedirectToPage();
        }
    }
}