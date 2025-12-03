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
            Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid || NewDevice == null)
                return RedirectToAction("Get");

            var result = await _service.AddDevice(NewDevice);
            if (result.IsSuccessful)
                return RedirectToPage("./Index");
            return Page();
        }
    }
}
