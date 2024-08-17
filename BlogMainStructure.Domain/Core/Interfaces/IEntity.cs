using BlogMainStructure.Domain.Enums;

namespace BlogMainStructure.Domain.Core.Interfaces
{
    // Represents a base entity with an identifier and status.
    public interface IEntity
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }

    }
}
