using TechInventory.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechInventory.Services.Maintenance;
using TechInventory.Services.Device;

namespace TechInventory.Pages.Maintenance
{
    public class EditModel(IMaintenanceRecordService service, IDeviceService deviceService) : PageModel
    {
        private readonly IMaintenanceRecordService _service = service;
        private readonly IDeviceService _deviceService = deviceService;

        [BindProperty]
        public MaintenanceRecord Record { get; set; } = default!;
        public List<Models.Device> Devices { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Record = await _service.GetRecordById(id);

            if (Record == null)
            {
                TempData["Message"] = "Nenhum registro foi encontrado";
                return RedirectToPage("/Maintenance/Index");
            }

            Devices = await _deviceService.GetAllDevices();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Os dados não eram válidos!";
                return RedirectToPage("/Maintenance/Index");
            }

            Record.MaintenanceRecordId = id;
            var result = await _service.UpdateRecord(Record);
            if (result.IsSuccessful)
                TempData["Message"] = "Registro de manutenção atualizado com sucesso!";
            else
                TempData["Message"] = result.Message;

            return RedirectToPage("/Maintenance/Index");
        }
    }
}
