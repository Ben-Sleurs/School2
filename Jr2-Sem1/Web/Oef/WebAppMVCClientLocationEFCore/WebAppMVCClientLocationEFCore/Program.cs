using Microsoft.EntityFrameworkCore;
using System.Text;
using WebAppMVCClientLocationEFCore.Data;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("ClientLocationContext");
//var sb = new StringBuilder();
//sb.Append("Server = (localdb)\\mssqllocaldb;");
//sb.Append("Database = ClientLocationData;");
//sb.Append("Trusted_Connection = True;");
//sb.Append("MultipleActiveResultSets=true");
//var connString = sb.ToString();
builder.Services.AddDbContext<ClientLocationContext>(options => options.UseSqlServer(connString));
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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
