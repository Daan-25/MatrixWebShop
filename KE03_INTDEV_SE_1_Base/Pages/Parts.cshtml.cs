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
            
            if (Part == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}