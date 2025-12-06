using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechInventory.Services.Device;

namespace TechInventory.Pages.Device
{
    public class DeleteModel(IDeviceService service) : PageModel
    {
        private readonly IDeviceService _service = service;

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var result = await _service.DeleteDevice(id);
            if (!result.IsSuccessful)
                TempData["Message"] = result.Message;
            return RedirectToPage("/Device/Index");
        }
    }
}
