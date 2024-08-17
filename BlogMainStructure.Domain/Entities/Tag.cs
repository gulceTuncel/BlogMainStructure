using BlogMainStructure.Domain.Core.BaseEntities;

namespace BlogMainStructure.Domain.Entities
{
    public class Tag : AuditableEntity
    {
        public Tag()
        {
            Articles = new HashSet<Article>();
        }

        public string Name { get; set; }
        public virtual ICollection<Article> Articles { get; set; }


    }
}
