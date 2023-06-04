using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PeopleApp.Data;
using PeopleApp.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration[
        "ConnectionStrings:PeopleConnection"]);
    opts.EnableSensitiveDataLogging(true);
});
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<IPersonRepository, PersonDbRepository>();
builder.Services.AddScoped<ILocationRepository, LocationDbRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
} // Configure the HTTP request pipeline.

app.UseBlazorFrameworkFiles();
app.UseDeveloperExceptionPage();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    //Enable attribute based routing for controllers:
    endpoints.MapControllers();
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToController("/manage/{*path:nonfile}", "Index", "Blazor");
    endpoints.MapFallbackToFile("/manage-webassembly/{*path:nonfile}", "index.html");
});

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DataContext>();
    SeedData.SeedDatabase(context);
}

app.Run();