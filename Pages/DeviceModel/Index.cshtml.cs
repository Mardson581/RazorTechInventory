using TechInventory.Services.DeviceModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechInventory.Pages.DeviceModel
{
    public class IndexModel(IDeviceModelService service) : PageModel
    {
        private readonly IDeviceModelService _service = service;
        public List<Models.DeviceModel> DeviceModels { get; set; } = default!;

        public async void OnGetAsync()
        {
            DeviceModels = await _service.GetAllDeviceModels();
        }
    }
}
