using TechInventory.Services.Device;
using TechInventory.Services.Maintenance;
using TechInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechInventory.Pages.Maintenance
{
    public class CreateModel(IMaintenanceRecordService service, IDeviceService deviceService) : PageModel
    {
        private readonly IDeviceService _deviceService = deviceService;
        private readonly IMaintenanceRecordService _service = service;

        [BindProperty]
        public MaintenanceRecord Record { get; set; } = default!;

        public List<Models.Device> Devices { get; set; } = [];

        public async void OnGetAsync()
        {
            Devices = await _deviceService.GetAllDevices();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
                TempData["Message"] = "Os dados não eram válidos!";

            var result = await _service.CreateRecord(Record);
            if (result.IsSuccessful)
                TempData["Message"] = "Registro de manutenção cadastrado com sucesso!";
            else
                TempData["Message"] = result.Message;

            return RedirectToPage("/Maintenance/Index");
        }
    }
}
