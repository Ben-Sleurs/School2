using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCAuthentication.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = options.User.AllowedUserNameCharacters + " ";
    options.User.RequireUniqueEmail = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication()
    .AddFacebook(fbOpts =>
    {
        //appid en secret van Kristof Evaert
        fbOpts.AppId = "199180349400081";
        fbOpts.AppSecret = "316d756e2ac49e51f04ac566408a10bb";
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
