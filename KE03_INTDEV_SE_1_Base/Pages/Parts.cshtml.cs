using DataAccessLayer;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class PartsModel : PageModel
    {
        private readonly IPartRepository _partRepository;

        public Part? Part { get; set; }

        public PartsModel(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Part = await _partRepository.GetByIdAsync(id);
            if (Part == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int id, int quantity = 1)
        {
            var part = await _partRepository.GetByIdAsync(id);
            if (part == null) return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();
            var existingItem = cart.FirstOrDefault(c => c.Part.Id == id);

            if (existingItem != null)
            {
                existingItem.Aantal += quantity;
            }
            else
            {
                cart.Add(new CartItem { Part = part, Aantal = quantity });
            }

            HttpContext.Session.SetObjectAsJson("cart", cart);

            return RedirectToPage("/Cart");
        }
    }
}