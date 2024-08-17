using BlogMainStructure.Domain.Core.Interfaces;
using BlogMainStructure.Domain.Enums;

namespace BlogMainStructure.Domain.Core.BaseEntities
{
    // Provides a base implementation for entities with creation and update information.
    public class BaseEntity : IUpdatableEntity
    {
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public Status Status { get; set; }
    }
}
