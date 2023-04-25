using MVCEndpointRouting.CustomConstraint;
using MVCEndpointRouting.Models.ViewModels;
using MVCEndpointRouting.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RouteOptions>(options =>
    options.ConstraintMap.Add("allowedFirstNames", typeof(FirstNameConstraint)));
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IRoutingRepository, RoutingRepository>();
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


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "intConstraint",
        pattern: "{controller=Home}/{action=Index}/{id:allowedFirstNames?}");
});
app.Run();
