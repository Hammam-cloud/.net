using MyRazorApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register services BEFORE builder.Build()
builder.Services.AddRazorPages();

// ✅ Add DbContext with MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Add session-related services
builder.Services.AddDistributedMemoryCache(); // Required for session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build(); // ✅ Build AFTER adding services

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseSession();          // ✅ Enable session before authorization
app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages().WithStaticAssets();

app.MapGet("/", context =>
{
    context.Response.Redirect("/SignIn");
    return Task.CompletedTask;
});

app.Run();
