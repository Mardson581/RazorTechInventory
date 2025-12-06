using TechInventory.Models;
using TechInventory.Services.DeviceModel;
using TechInventory.Services.Brand;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechInventory.Pages.DeviceModel
{
    public class EditModel(IDeviceModelService service, IBrandService brandService) : PageModel
    {
        private readonly IDeviceModelService _service = service;
        private readonly IBrandService _brandService = brandService;
        public List<Models.Brand> Brands { get; set; } = default!;

        [BindProperty]
        public Models.DeviceModel DeviceModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            DeviceModel = await _service.GetDeviceModelById(id);
            if (DeviceModel == null)
            {
                TempData["Message"] = "O dispositivo não foi encontrado!";
                return RedirectToPage("/DeviceModel/Index");
            }

            Brands = await _brandService.GetAllBrands();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                TempData["Message"] = "Os dados não eram válidos!";

            DeviceModel.DeviceModelId = id;
            var result = await _service.UpdateDeviceModel(DeviceModel);
            if (result.IsSuccessful)
                TempData["Message"] = "Modelo atualizado com sucesso!";
            else
                TempData["Message"] = result.Message;
            return RedirectToPage("/DeviceModel/Index");
        }
    }
}
