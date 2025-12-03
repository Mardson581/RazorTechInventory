using Microsoft.AspNetCore.Mvc.RazorPages;
using TechInventory.Services.Device;

namespace TechInventory.Pages.Device
{
    public class IndexModel(IDeviceService service) : PageModel
    {
        private readonly IDeviceService _service = service;
        public List<Models.Device> Devices = default!;

        public async void OnGet()
        {
            Devices = await _service.GetAllDevices();
        }
    }
}
