using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MyRazorApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }

    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string role { get; set; } = string.Empty;
    }
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
    }
    public class Service
    {
        public int Id { get; set; }

        [Required]
        public string StudentName { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [Range(1, 100)]
        public int Age { get; set; }

        [Required]
        public string ServiceType { get; set; }
        public int UserId { get; set; }
        public string AcademicYear { get; set; }
        public DateTime? DateAdded { get; set; }
    }
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int PublishedYear { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public int Price { get; set; }

    }
public class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int BookId { get; set; }
    public Book Book { get; set; }

    public DateTime OrderDate { get; set; } = DateTime.Now;
}

}
