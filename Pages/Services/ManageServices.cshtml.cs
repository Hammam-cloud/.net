using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MyRazorApp.Pages.Services
{
    public class ManageServicesModel : PageModel
    {
        private readonly AppDbContext _context;

        public ManageServicesModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Service> Services { get; set; }

        public async Task OnGetAsync()
        {
            Services = await _context.Services.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int id, DateTime? date)
        {
            if (date == null)
            {
                ModelState.AddModelError(string.Empty, "Date is required.");
                await OnGetAsync(); // Reload data
                return Page();
            } 

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            service.DateAdded = date;
            await _context.SaveChangesAsync();

            return RedirectToPage(); // Refresh page
        }
    }
}
