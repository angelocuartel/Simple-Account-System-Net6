using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleAccountSystem.Entity;

namespace SimpleAccountSystem.Data.Configurations
{
    internal class PageModuleConfiguration : IEntityTypeConfiguration<PageModule>
    {
        public void Configure(EntityTypeBuilder<PageModule> builder)
        {
            builder.ToTable("PageModule");
            builder.HasKey(i => i.PageModuleId);
        }
    }
}
