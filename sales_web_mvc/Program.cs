using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sales_web_mvc.Data;
using sales_web_mvc.Services;
using System.Configuration;
var builder = WebApplication.CreateBuilder(args);

// Vincular o BD
builder.Services.AddDbContext<sales_web_mvcContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("sales_web_mvcContext"),
        new MySqlServerVersion(new Version(8, 0, 34))
    )
);

// Adicionar o SeedingService para popular o bd
builder.Services.AddScoped<SeedingService>();

builder.Services.AddScoped<SellerService>(); // Classe SellerService

builder.Services.AddScoped<DepartmentService>(); // Classe Department Service

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Aqui, caso o projeto esteja rodando de forma de desenvolvimento,ira chamar o seeding service, se nao, ira apresentar o erro
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    using (var scope = app.Services.CreateScope())
    {
        var seedingService = scope.ServiceProvider.GetRequiredService<SeedingService>();
        seedingService.Seed();
    }
}
else
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
