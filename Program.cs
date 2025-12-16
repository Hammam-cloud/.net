using Microsoft.EntityFrameworkCore;
using MyRazorApp.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages()
       .AddRazorRuntimeCompilation();  // enables runtime compilation

// If you also have controllers
builder.Services.AddControllersWithViews()
       .AddMvcOptions(options =>
       {
           options.SuppressAsyncSuffixInActionNames = true;
       })
       .AddRazorRuntimeCompilation();
// Razor pages

// Read Elastic Beanstalk RDS environment variables
var host = Environment.GetEnvironmentVariable("DB_HOST");
var port = Environment.GetEnvironmentVariable("DB_PORT");
var name = Environment.GetEnvironmentVariable("DB_NAME");
var user = Environment.GetEnvironmentVariable("DB_USER");
var pass = Environment.GetEnvironmentVariable("DB_PASSWORD");

Console.WriteLine($"DB_HOST from ENV = {host}");

// Local fallback for development (optional)
if (builder.Environment.IsDevelopment())
{
    host = "localhost";
    port = "3306";
    name = "myappdb";
    user = "root";
    pass = "";
}
else if (string.IsNullOrEmpty(host))
{
    throw new Exception("Database environment variables are missing");
}


var connectionString =
    $"Server={host};Port={port};Database={name};User={user};Password={pass};";

try
{
    using var testConn = new MySqlConnector.MySqlConnection(connectionString);
    testConn.Open();
    Console.WriteLine("✅ Database connection successful!");
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Database connection failed: {ex.Message}");
    throw;
}
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

var listenPort = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://0.0.0.0:{listenPort}");

