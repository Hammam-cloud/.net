using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyRazorApp.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;

namespace MyRazorApp.Pages.Services
{
    public class ServiceListModel : PageModel
    {
        private readonly AppDbContext _context;

        public ServiceListModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Service> UserServices { get; set; }

        public void OnGet()
        {
            // Fetch the userId from the session
            var userId = HttpContext.Session.GetInt32("UserId");

            // If user is logged in (userId is not null), get their services
            if (userId.HasValue)
            {
                UserServices = _context.Services
                    .Where(s => s.UserId == userId.Value)
                    .ToList();
            }
            else
            {
                UserServices = new List<Service>();
            }
        }
    }
}
