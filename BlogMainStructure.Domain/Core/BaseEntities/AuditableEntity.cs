using BlogMainStructure.Domain.Core.Interfaces;

namespace BlogMainStructure.Domain.Core.BaseEntities
{
    // Extends the BaseEntity to include information about deletion.
    public class AuditableEntity : BaseEntity, IDeletableEntity
    {
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
