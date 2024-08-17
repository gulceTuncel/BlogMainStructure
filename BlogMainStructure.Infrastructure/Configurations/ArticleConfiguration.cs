using BlogMainStructure.Domain.Core.BaseEntityConfiguration;
using BlogMainStructure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogMainStructure.Infrastructure.Configurations
{
    public class ArticleConfiguration : AuditableEntityConfiguration<Article>
    {
        public override void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.Property(a => a.Content).IsRequired();
            builder.Property(a => a.Title).IsRequired().HasMaxLength(256);
            base.Configure(builder);
        }
    }
}
