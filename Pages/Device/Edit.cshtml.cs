using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechInventory.Services.Device;
using TechInventory.Services.DeviceModel;

namespace TechInventory.Pages.Device
{
    public class EditModel(IDeviceService service, IDeviceModelService deviceModelService) : PageModel
    {
        private readonly IDeviceService _service = service;
        private readonly IDeviceModelService _deviceModelService = deviceModelService;

        [BindProperty]
        public Models.Device Device { get; set; } = default!;

        public List<Models.DeviceModel> DeviceModels { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Device = await _service.GetDeviceById(id);

            if (Device == null)
            {
                TempData["Message"] = "Dispositivo não encontrado";
                return RedirectToPage("/Device/Index");
            }

            DeviceModels = await _deviceModelService.GetAllDeviceModels();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                TempData["Message"] = "Ocorreu um erro ao validar os dados";

            Models.DeviceModel deviceModel = await _deviceModelService.GetDeviceModelById(id);
            if (deviceModel == null)
                TempData["Message"] = "O modelo do dispositivo não foi encontrado!";
                
            Device.DeviceId = id;
            var result = await _service.UpdateDevice(Device);

            if (result.IsSuccessful)
                TempData["Message"] = "Dispositivo atualizado com sucesso";
            else
                TempData["Message"] = result.Message;

            return RedirectToPage("/Device/Index");
        }
    }
}
