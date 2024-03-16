using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleAccountSystem.Data.Configurations;
using SimpleAccountSystem.Entity;

namespace SimpleAccountSystem.Data;

public class SimpleAccountSystemMvcContext : IdentityDbContext<IdentityUser>
{
    public DbSet<PageModule>? PageModules { get; set; }
    public SimpleAccountSystemMvcContext(DbContextOptions<SimpleAccountSystemMvcContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new PageModuleConfiguration());
    }
}
