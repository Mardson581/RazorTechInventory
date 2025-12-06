using TechInventory.Services.DeviceModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechInventory.Pages.DeviceModel
{
    public class DeleteModel(IDeviceModelService service) : PageModel
    {
        private readonly IDeviceModelService _service = service;

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var result = await _service.DeleteDeviceModel(id);

            if (result.IsSuccessful)
                TempData["Message"] = "O modelo de dispositivo foi excluído com sucesso!";
            else
                TempData["Message"] = result.Message;
            return RedirectToPage("/DeviceModel/Index");
        }
    }
}
