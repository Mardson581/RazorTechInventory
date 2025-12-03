using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechInventory.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Models.Device NewDevice { get; set; } = default!;

        public List<Models.DeviceModel> DeviceModels { get; set; } = default!;

        public void OnGet()
        {
            DeviceModels = [];
            Page();
        }

        public IActionResult OnPostAsyncCreate()
        {
            return RedirectToAction("Get");
        }
    }
}
