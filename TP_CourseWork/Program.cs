using Microsoft.EntityFrameworkCore;
using TP_CourseWork.Models;
using TP_CourseWork.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient(c=> NetSingleton.Instance);
builder.Services.AddDbContext<HistoryContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=history_db;Username=postgres;Password=root"));

builder.Services.AddScoped<IPostgreSQLRepository, PostgreSQLRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();    
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
