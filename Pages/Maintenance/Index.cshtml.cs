using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechInventory.Models;
using TechInventory.Services.Maintenance;

namespace TechInventory.Pages.Maintenance
{
    public class IndexModel(IMaintenanceRecordService service) : PageModel
    {
        private readonly IMaintenanceRecordService _service = service;
        public List<MaintenanceRecord> Records { get; set; } = [];

        public async void OnGetAsync()
        {
            Records = await _service.GetAllRecordsWithDevices();
        }
    }
}
