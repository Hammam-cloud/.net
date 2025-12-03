using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorApp.Data;

namespace MyRazorApp.Pages.Books
{
    public class ShopModel : PageModel
    {
        private readonly AppDbContext _context;
        public string Message { get; set; }

        public ShopModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> Books { get; set; }

        public async Task OnGetAsync()
        {
            Books = await _context.Books.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int bookId)
        {
            var userId = HttpContext.Session.GetInt32("UserId"); 

            if (userId == null)
            {
                return RedirectToPage("/Login");
            }

            var order = new Order
            {
                UserId = userId.Value,
                BookId = bookId,
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            
             return RedirectToPage("/Books/OrderList");
          
             
        }
    }
}
