using Microsoft.EntityFrameworkCore;
using Picklr.Models;

var builder = WebApplication.CreateBuilder(args);

// Add MVC services
builder.Services.AddControllersWithViews();


// Add Memory Cache (same technique used in class)
builder.Services.AddMemoryCache();


// Add Session services
builder.Services.AddSession();


builder.Services.AddDbContext<PicklrContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("PicklrContext")));

var app = builder.Build();


// Configure HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();


app.UseRouting();


app.UseSession();

app.UseAuthorization();


// Admin area route
app.MapAreaControllerRoute(
    name: "admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");


// Default client route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();