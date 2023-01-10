using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TP_CourseWork.Models;
using TP_CourseWork.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IStrategyByPicture>(c=> NetSingleton.Instance as IStrategyByPicture);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FaceDetection", Version = "v1" });
});
builder.Services.AddDbContext<HistoryContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=history_db;Username=postgres;Password=root"));

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
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FaceDetection v1"));

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
