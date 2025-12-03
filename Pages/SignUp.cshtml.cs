using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorApp.Data;


namespace MyRazorApp.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly AppDbContext _context;

        public SignUpModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string role {get; set; }=string.Empty;

        public string Message { get; set; } = string.Empty;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Password != ConfirmPassword)
            {
                Message = "Passwords do not match.";
                return Page();
            }

            if (_context.Users.Any(u => u.Username == Username || u.Email == Email))
            {
                Message = "Username or email already exists.";
                return Page();
            }
            

            var user = new User
            {
                Username = Username,
                Email = Email,
                Password = Password,
                role ="client"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            Message = "Registration successful!";
            return RedirectToPage("/SignIn");
        }
    }
}
