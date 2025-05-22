using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DataAccessLayer.Models;

public class RegisterModel : PageModel
{
    private readonly UserService _userService;

    public RegisterModel(UserService userService)
    {
        _userService = userService;
    }

    [BindProperty]
    public string Gebruikersnaam { get; set; }

    [BindProperty]
    public string Wachtwoord { get; set; }

    public string Foutmelding { get; set; }

    public IActionResult OnPost()
    {
        var gebruiker = new User
        {
            Gebruikersnaam = Gebruikersnaam,
            Wachtwoord = Wachtwoord
        };

        if (_userService.Registreer(gebruiker))
        {
            TempData["Gebruiker"] = Gebruikersnaam;
            return RedirectToPage("/Index");
        }

        Foutmelding = "Gebruikersnaam bestaat al.";
        return Page();
    }
}