using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace MyRazorApp.Pages.Orders
{
    public class OrderListModel : PageModel
    {
        private readonly AppDbContext _context;

        public OrderListModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Order> Orders { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Assume user ID is stored in session (or change to actual user auth logic)
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            Orders = await _context.Orders
                .Include(o => o.Book)
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return Page();
        }
    }
}
