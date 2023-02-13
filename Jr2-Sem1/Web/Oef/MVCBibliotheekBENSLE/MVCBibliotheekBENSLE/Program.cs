using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCBibliotheekBENSLE.Data;
using MVCBibliotheekBENSLE.Data.DefaultData;
using MVCBibliotheekBENSLE.PasswordValidators;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("AppDbConn");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(passwordOptions =>
{
    passwordOptions.Password.RequiredLength = 5;
    passwordOptions.Password.RequireUppercase = true;
    passwordOptions.Password.RequireLowercase = true;
    passwordOptions.Password.RequireNonAlphanumeric = false;
})
    .AddEntityFrameworkStores<AppDbContext>();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Reservaties}/{action=Index}/{id?}");
SeedData.EnsurePopulated(app);
app.Run();
