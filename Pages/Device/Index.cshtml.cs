using Microsoft.AspNetCore.Mvc.RazorPages;
using TechInventory.Services.Device;

namespace TechInventory.Pages.Device
{
    public class IndexModel(IDeviceService service) : PageModel
    {
        private readonly IDeviceService _service = service;
        public List<Models.Device> Devices = [];

        public async void OnGet()
        {
            try
            {
                Devices = await _service.GetAllDevices();
            }
            catch (Exception ex)
            {
                TempData["Message"] = ex.Message;
            }
        }
    }
}
