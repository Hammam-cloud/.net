using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MyRazorApp.Pages.Students
{
    public class EditStudentModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditStudentModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Student = await _context.Students.FindAsync(id);

            if (Student == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToPage("/Students/Students");
        }
    }
}
