using TechInventory.Services.Maintenance;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechInventory.Pages.Maintenance
{
    public class DeleteModel(IMaintenanceRecordService service) : PageModel
    {
        private readonly IMaintenanceRecordService _service = service;

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var result = await _service.DeleteRecord(id);
            if (!result.IsSuccessful)
                TempData["Message"] = result.Message;

            return RedirectToPage("/Maintenance/Index");
        }
    }
}
