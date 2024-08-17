namespace BlogMainStructure.Domain.Core.Interfaces
{
    // Represents an entity that can be deleted, with information about deletion.
    public interface IDeletableEntity
    {
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
