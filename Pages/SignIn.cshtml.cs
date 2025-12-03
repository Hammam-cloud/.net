using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorApp.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MyRazorApp.Pages
{
    public class SignInModel : PageModel
    {
        private readonly AppDbContext _context;

        public SignInModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string Message { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                Message = "Please enter username and password.";
                return Page();
            }
            var user = _context.Users
                .FirstOrDefault(u => u.Username == Username && u.Password == Password);
                

            if (user != null)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);

                if (user.role == "admin")
                {
                    return RedirectToPage("/Students/Students");
                }
                else if (user.role == "client")
                {
                    return RedirectToPage("/Services/AddService");
                }
            }

            Message = "Invalid login!";
            return Page();
        }
    }
}
