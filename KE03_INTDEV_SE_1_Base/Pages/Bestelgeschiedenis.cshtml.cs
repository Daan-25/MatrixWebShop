using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer;
using DataAccessLayer.Models;

public class BestelgeschiedenisModel : PageModel
{
    private readonly MatrixIncDbContext _context;

    public BestelgeschiedenisModel(MatrixIncDbContext context)
    {
        _context = context;
    }

    public List<Order> Bestellingen { get; set; } = new();

    public void OnGet()
    {
        var gebruikerIdClaim = User.FindFirst("GebruikerId")?.Value;

        if (int.TryParse(gebruikerIdClaim, out int gebruikerId))
        {
            var gebruiker = _context.Users
                .Include(u => u.Customer)
                .FirstOrDefault(u => u.Id == gebruikerId);

            if (gebruiker?.Customer != null)
            {
                Bestellingen = _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Part)
                    .Where(o => o.CustomerId == gebruiker.Customer.Id)
                    .ToList();
            }
        }
    }
}