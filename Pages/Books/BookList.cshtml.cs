using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorApp.Data;
namespace MyRazorApp.Pages.Books
{
    public class BookListModel : PageModel
    {
        private readonly AppDbContext _context;

        public BookListModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Book> Books { get; set; }

        public async Task OnGetAsync()
        {
            Books = await _context.Books.ToListAsync();
        }
    }
}
