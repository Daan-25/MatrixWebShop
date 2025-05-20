using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IPartRepository _partsRepository;

        public IList<Part> Parts { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IPartRepository partRepository)
        {
            _logger = logger;
            _partsRepository = partRepository;
            Parts = new List<Part>();
        }

        public void OnGet()
        {            
            Parts = _partsRepository.GetAllParts().ToList();                            
            _logger.LogInformation($"getting all {Parts.Count} parts");
        }
    }
}
