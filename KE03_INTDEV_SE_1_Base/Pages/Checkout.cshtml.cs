using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class CheckoutModel : PageModel
    {
        [BindProperty]
        public string Naam { get; set; }

        [BindProperty]
        public string Adres { get; set; }

        [BindProperty]
        public string Betaalmethode { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            
            TempData["Naam"] = Naam;
            TempData["Adres"] = Adres;
            TempData["Betaalmethode"] = Betaalmethode;
            
            HttpContext.Session.Remove("cart");
            
            return RedirectToPage("/OrderConfirmation");
        }
    }
}