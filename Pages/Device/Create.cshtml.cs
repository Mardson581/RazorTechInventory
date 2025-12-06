using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechInventory.Services.Device;
using TechInventory.Services.DeviceModel;

namespace TechInventory.Pages.Device
{
    public class CreateModel(IDeviceService service, IDeviceModelService deviceModelService) : PageModel
    {
        private readonly IDeviceService _service = service;
        private readonly IDeviceModelService _deviceModelService = deviceModelService;

        [BindProperty]
        public Models.Device NewDevice { get; set; } = default!;

        public List<Models.DeviceModel> DeviceModels { get; set; } = default!;

        public async void OnGetAsync()
        {
            DeviceModels = await _deviceModelService.GetAllDeviceModels();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid || NewDevice == null)
                TempData["Message"] = "Ocorreu um erro ao validar os dados";

            var result = await _service.AddDevice(NewDevice);
            if (result.IsSuccessful)
                TempData["Message"] = "Dispositivo adicionado com sucesso";
            else
                TempData["Message"] = result.Message;

            return RedirectToPage("/Device/Index");
        }
    }
}
