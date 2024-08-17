using BlogMainStructure.Domain.Core.BaseEntities;

namespace BlogMainStructure.Domain.Entities
{
    // Represents a regular user in the system with auditing capabilities.
    public class Author : AuditableEntity
    {
        public Author()
        {
            Articles = new HashSet<Article>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[]? Image { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
