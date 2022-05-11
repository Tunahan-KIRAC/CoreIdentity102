using CoreIdentity102.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr")));
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
    

//***********Sql i�in ba�lant�y� sa�lamak i��in son a�ama buras� �nce classlar� yazd�k sonra dbcontext s�n�f�m�z� yazd�k sonra apssetting.json i�ine connection string imizi yazd�k en son a�amada buraya builder olarak yazd�k ve sql ba�lant�m�z tamamlanm�� oldu.********************



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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
