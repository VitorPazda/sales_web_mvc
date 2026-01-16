using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sales_web_mvc.Data;
using System.Configuration;
var builder = WebApplication.CreateBuilder(args);

// Vincular o BD
builder.Services.AddDbContext<sales_web_mvcContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("sales_web_mvcContext"),
        new MySqlServerVersion(new Version(8, 0, 34))
    )
);


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
