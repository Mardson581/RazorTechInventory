using TechInventory.Models;
using TechInventory.Services.DeviceModel;
using TechInventory.Services.Brand;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechInventory.Pages.DeviceModel
{
    public class CreateModel(IDeviceModelService service, IBrandService brandService) : PageModel
    {
        private readonly IDeviceModelService _service = service;
        private readonly IBrandService _brandService = brandService;
        public List<Models.Brand> Brands { get; set; } = default!;

        [BindProperty]
        public Models.DeviceModel NewDeviceModel { get; set; } = default!;

        public async void OnGetAsync()
        {
            Brands = await _brandService.GetAllBrands();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid || NewDeviceModel == null)
                TempData["Message"] = "Não foi possível cadastrar o modelo (verifique os dados)";
            
            var result = await _service.CreateDeviceModel(NewDeviceModel);
            if (result.IsSuccessful)
                TempData["Message"] = "Modelo cadastrado com sucesso!";
            else
                TempData["Message"] = result.Message;

            return RedirectToPage("/DeviceModel/Index");
        }
    }
}