using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyRazorApp.Data;


namespace MyRazorApp.Pages.Students
{
    public class AddStudentModel : PageModel
    {
        private readonly AppDbContext _context;

        public AddStudentModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student NewStudent { get; set; }

        public string ErrorMessage { get; set; } // To store error message

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if a student with the same email already exists
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.Email == NewStudent.Email);

            if (existingStudent != null)
            {
                // If email is found, set error message and return the page
                ErrorMessage = "Email is already in use. Please choose another one.";
                return Page();
            }

            // If no existing student with the same email, add the new student
            if (ModelState.IsValid)
            {
                _context.Students.Add(NewStudent);
                await _context.SaveChangesAsync();
                return RedirectToPage("/Students/Students"); // Redirect to Students page after adding the student
            }

            return Page();
        }
    }
}
