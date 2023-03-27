using Microsoft.EntityFrameworkCore;
using PeopleApp.Data;
using PeopleApp.Data.DefaultData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connString = builder.Configuration.GetConnectionString("PeopleConnection");
builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(connString);
    opts.EnableSensitiveDataLogging(true);
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
    pattern: "{controller=Home}/{action=Index}/{id?}");
SeedData.SeedDatabase(app);
app.Run();
