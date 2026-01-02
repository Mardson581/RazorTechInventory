using TechInventory.Services.Brand;
using TechInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechInventory.Pages.Brand
{
    public class CreateModel(IBrandService service) : PageModel
    {
        private readonly IBrandService _service = service;

        [BindProperty]
        public Models.Brand NewBrand { get; set; } = default!;

        public void OnGet()
        {
            Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid || NewBrand == null)
                return RedirectToAction("Get");

            var result = await _service.CreateBrand(NewBrand);
            if (result.IsSuccessful)
                return RedirectToPage("./Index");

            TempData["Message"] = result.Message;
            return RedirectToAction("Get");
        }
    }
}
