using Microsoft.EntityFrameworkCore;
using MyRazorApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Razor pages
builder.Services.AddRazorPages();

// Read Elastic Beanstalk RDS environment variables
var host = Environment.GetEnvironmentVariable("RDS_HOSTNAME");
var port = Environment.GetEnvironmentVariable("RDS_PORT");
var name = Environment.GetEnvironmentVariable("RDS_DB_NAME");
var user = Environment.GetEnvironmentVariable("RDS_USERNAME");
var pass = Environment.GetEnvironmentVariable("RDS_PASSWORD");

// Local fallback for development (optional)
if (host == null)
{
    host = "localhost";
    port = "3306";
    name = "myappdb";
    user = "root";
    pass = "";
}

var connectionString =
    $"Server={host};Port={port};Database={name};User={user};Password='';";

// Register EF Core MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Add session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
