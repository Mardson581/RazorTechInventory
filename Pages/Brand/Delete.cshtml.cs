using TechInventory.Services.Brand;
using TechInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechInventory.Pages.Brand
{
    public class DeleteModel(IBrandService service) : PageModel
    {
        private readonly IBrandService _service = service;

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var result = await _service.DeleteBrand(id);
            if (result.IsSuccessful)
                ViewData["Message"] = result.Message;
                
            return RedirectToPage("/Brand/Index");
        }
    }
}
