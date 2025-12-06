using TechInventory.Services.Brand;
using TechInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechInventory.Pages.Brand
{
    public class EditModel(IBrandService service) : PageModel
    {
        private readonly IBrandService _service = service;

        [BindProperty]
        public Models.Brand Brand { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Brand = await _service.GetBrandById(id);

            if (Brand == null)
            {
                ViewData["Message"] = "Nenhuma marca foi encontrada";
                return RedirectToPage("/Brand/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Brand.BrandId = id;

            var result = await _service.UpdateBrand(Brand);
            if (result.IsSuccessful)
                TempData["Message"] = "Marca atualizada com sucesso!";
            else
                ViewData["Message"] = result.Message;
            return RedirectToPage("/Brand/Index");
        }
    }
}
