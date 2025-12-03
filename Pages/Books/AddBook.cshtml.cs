using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorApp.Data;

namespace MyRazorApp.Pages.Books
{
    public class AddBookModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddBookModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Book NewBook { get; set; }

        public string Message { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
             bool exists = await _context.Books.AnyAsync(b =>
                b.Title == NewBook.Title &&
                b.Author == NewBook.Author &&
                b.PublishedYear == NewBook.PublishedYear);

            if (exists)
            {
                Message = "Book already exists.";
                return Page();
            }

            _context.Books.Add(NewBook);
            await _context.SaveChangesAsync();

            Message = "Book added successfully!";
            ModelState.Clear();
            NewBook = new Book(); // Reset the form
            return Page();
        }
    }
}
