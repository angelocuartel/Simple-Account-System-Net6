using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SimpleAccountSystem.Mvc.Configurations.FluentEmail;
using SimpleAccountSystem.Mvc.Configurations.Identity;
using SimpleAccountSystem.Mvc.Configurations.Session;
using SimpleAccountSystem.Mvc.Data;
using SimpleAccountSystem.Mvc.Services.FluentEmail;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SimpleAccountSystemMvcContextConnection") ?? throw new InvalidOperationException("Connection string 'SimpleAccountSystemMvcContextConnection' not found.");

builder.Services.AddDbContext<SimpleAccountSystemMvcContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    // add context where identity can use to store identity ralated data
    .AddEntityFrameworkStores<SimpleAccountSystemMvcContext>();

builder.Services.AddDistributedMemoryCache();

// custom service config for identity password and session
builder.Services.SetSecuredPasswordPolicy();
builder.Services.SetIdentitySessionCookiesConfiguration();

//fluent email
builder.Services.BuildFluentEmailDependencies(builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IEmailSender,FluentEmailService>();

builder.Services.AddSessionWithDefaultConfiguration();

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
