using BlogMainStructure.Domain.Core.BaseEntities;
using BlogMainStructure.Domain.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogMainStructure.Domain.Core.BaseEntityConfiguration
{
    //Configures the AuditableEntity properties for Entity Framework Core.
    public class AuditableEntityConfiguration<TEntity> : BaseEntityConfiguration<TEntity> where TEntity : AuditableEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.DeletedBy).IsRequired(false);
            builder.Property(x => x.DeletedDate).IsRequired(false);

            base.Configure(builder);
        }
    }
}
