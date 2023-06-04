using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleAccountSystem.Mvc.Configurations.Identity;
using SimpleAccountSystem.Mvc.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SimpleAccountSystemMvcContextConnection") ?? throw new InvalidOperationException("Connection string 'SimpleAccountSystemMvcContextConnection' not found.");

builder.Services.AddDbContext<SimpleAccountSystemMvcContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<SimpleAccountSystemMvcContext>();

// custom service config for identity password
builder.Services.SetSecuredPasswordPolicy();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
