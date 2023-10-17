using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using MySqlConnector;
using SalesWebMVC.Data;
using System.Configuration;
using SalesWebMVC.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SalesWebMVCContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SalesWebMVCContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMVCContext' not found.")));


// conectando o Mysql
//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<SalesWebMVCContext>(options =>

//options.UseMySql(builder.Configuration.GetConnectionString("SalesWebMVCContext"), ServerVersion.AutoDetect("SalesWebMVCContext")

//?? throw new InvalidOperationException("Connection string 'SalesWebMVCContext' not found.")));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>(); // adicionando serviço no sistema de injeção de dependência

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Migration = script pra migrar e versionar a base de dados