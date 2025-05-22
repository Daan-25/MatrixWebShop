using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

public class LogoutModel : PageModel
{
    public async Task<IActionResult> OnGet()
    {
        await HttpContext.SignOutAsync("MyCookieAuth");
        return RedirectToPage("/Login");
    }
}