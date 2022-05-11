using CoreIdentity102.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConStr")));
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
    

//***********Sql için baðlantýyý saðlamak iççin son aþama burasý önce classlarý yazdýk sonra dbcontext sýnýfýmýzý yazdýk sonra apssetting.json içine connection string imizi yazdýk en son aþamada buraya builder olarak yazdýk ve sql baðlantýmýz tamamlanmýþ oldu.********************



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
