using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Models;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly MatrixIncDbContext _context;

        public CheckoutModel(MatrixIncDbContext context)
        {
            _context = context;
        }

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
            
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") ?? new List<CartItem>();

            if (cart.Count == 0)
                return RedirectToPage("/Cart");
            
            var gebruikerIdClaim = User.FindFirst("GebruikerId")?.Value;

            if (!int.TryParse(gebruikerIdClaim, out int gebruikerId))
                return RedirectToPage("/Login"); // eventueel foutafhandeling

            var gebruiker = _context.Users
                .Include(u => u.Customer)
                .FirstOrDefault(u => u.Id == gebruikerId);

            if (gebruiker?.Customer == null)
                return RedirectToPage("/Login"); // of foutmelding
            
            var order = new Order
            {
                CustomerId = gebruiker.Customer.Id,
                OrderDate = DateTime.Now,
                PaymentMethod = Betaalmethode, // Zorg dat dit veld bestaat in je model
                OrderItems = cart.Select(item => new OrderItem
                {
                    ProductId = item.Part.Id,
                    Aantal = item.Aantal
                }).ToList()
            };
            
            _context.Orders.Add(order);
            _context.SaveChanges();
            
            TempData["Naam"] = Naam;
            TempData["Adres"] = Adres;
            TempData["Betaalmethode"] = Betaalmethode;

            HttpContext.Session.Remove("cart");

            return RedirectToPage("/OrderConfirmation");
        }
    }
}