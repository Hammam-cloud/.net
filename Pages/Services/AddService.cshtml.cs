using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorApp.Data;

namespace MyRazorApp.Pages.Services
{
    public class AddServiceModel : PageModel
    {
        private readonly AppDbContext _context;
         [BindProperty]
         public Service NewService { get; set; } = new Service();


        public AddServiceModel(AppDbContext context)
        {
            _context = context;
        }

       
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
    {
      
             return RedirectToPage("/SignIn");
    }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            NewService.UserId=userId.Value;
            NewService.DateAdded =null; 
            _context.Services.Add(NewService);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Services/ServiceList");
        }
    }
}
