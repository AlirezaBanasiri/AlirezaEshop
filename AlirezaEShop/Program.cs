using AlirezaEShop.Data;
using AlirezaEShop.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region ConnectionString
builder.Services.AddDbContext<AlirezaEShopContext>(Options =>
{
    Options.UseSqlServer(@"Data Source=DESKTOP-DSQQ71B;Initial Catalog=AlirezaEShop;Integrated Security=True;TrustServerCertificate=True");
});
#endregion

#region IOC
builder.Services.AddScoped<IGroupRepositories, GroupRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
#endregion

#region Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(Option =>
{
    Option.LoginPath = "/Account/Login";
    Option.LogoutPath = "/Account/Logout";
    Option.ExpireTimeSpan = TimeSpan.FromDays(10);
});
#endregion

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
