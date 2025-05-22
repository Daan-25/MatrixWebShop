using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class OrderConfirmationModel : PageModel
    {
        public string Naam { get; set; }
        public string Adres { get; set; }
        public string Betaalmethode { get; set; }

        public void OnGet()
        {
            Naam = TempData["Naam"] as string;
            Adres = TempData["Adres"] as string;
            Betaalmethode = TempData["Betaalmethode"] as string;
        }
    }
}