using TechInventory.Models;
using TechInventory.Services.Brand;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechInventory.Pages.Brand
{
    public class IndexModel(IBrandService service) : PageModel
    {
        private readonly IBrandService _service = service;
        public IList<Models.Brand> BrandList { get; set; } = new List<Models.Brand>();

        public async Task OnGetAsync()
        {
            BrandList = await _service.GetAllBrands();
        }
    }
}
