using Microsoft.AspNetCore.Mvc.RazorPages;
using TechInventory.Services.Device;

namespace TechInventory.Pages
{
    public class DeviceModel(IDeviceService service) : PageModel
    {
        private readonly IDeviceService _service = service;
        public List<Models.Device> Devices = default!;

        public async void OnGet()
        {
            Devices = await _service.GetAllDevices();
        }
    }
}
