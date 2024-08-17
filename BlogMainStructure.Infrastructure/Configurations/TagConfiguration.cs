using BlogMainStructure.Domain.Core.BaseEntityConfiguration;
using BlogMainStructure.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMainStructure.Infrastructure.Configurations
{
    public class TagConfiguration : AuditableEntityConfiguration<Tag>
    {
        public override void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
            base.Configure(builder);
        }
    }
}
