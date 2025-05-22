using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using DataAccessLayer.Models;

public class LoginModel : PageModel
{
    private readonly UserService _userService;

    public LoginModel(UserService userService)
    {
        _userService = userService;
    }

    [BindProperty]
    public string Gebruikersnaam { get; set; }

    [BindProperty]
    public string Wachtwoord { get; set; }

    public string Foutmelding { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (_userService.Inloggen(Gebruikersnaam, Wachtwoord, out int gebruikerId))
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, Gebruikersnaam),
                new Claim("GebruikerId", gebruikerId.ToString())
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", principal);

            return RedirectToPage("/Index");
        }

        Foutmelding = "Ongeldige inloggegevens.";
        return Page();
    }
}