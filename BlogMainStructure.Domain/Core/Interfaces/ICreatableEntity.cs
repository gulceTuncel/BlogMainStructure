namespace BlogMainStructure.Domain.Core.Interfaces
{
    // Represents an entity that can be created, with information about creation.
    public interface ICreatableEntity : IEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
