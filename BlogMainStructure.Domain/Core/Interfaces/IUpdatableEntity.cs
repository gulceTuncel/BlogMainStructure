namespace BlogMainStructure.Domain.Core.Interfaces
{
    // Represents an entity that can be updated, including creation and update information.
    public interface IUpdatableEntity : ICreatableEntity
    {
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
