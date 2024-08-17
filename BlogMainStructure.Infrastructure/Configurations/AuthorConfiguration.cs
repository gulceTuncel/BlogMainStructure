using BlogMainStructure.Domain.Core.BaseEntityConfiguration;
using BlogMainStructure.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogMainStructure.Infrastructure.Configurations
{
    // Configures the entity mapping for Author entity.
    public class AuthorConfiguration : AuditableEntityConfiguration<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(128);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(128);
            builder.Property(a => a.Email).IsRequired().HasMaxLength(128);

            base.Configure(builder);
        }
    }
}
