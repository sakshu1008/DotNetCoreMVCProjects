using Microsoft.EntityFrameworkCore;
using SinglePageApplicationUsingjQueryAndAjax.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EmployeeDBContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeConnection")));
// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    // A property naming policy, or null to leave property names unchanged.
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

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
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();
