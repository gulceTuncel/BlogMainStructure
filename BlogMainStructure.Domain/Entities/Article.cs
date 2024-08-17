using BlogMainStructure.Domain.Core.BaseEntities;

namespace BlogMainStructure.Domain.Entities
{
    public class Article : AuditableEntity
    {

        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public DateTime PublishDate { get; set; }
        public byte[]? Image { get; set; }
        
        //Navigation 
        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public Guid? TagId { get; set; }
        public virtual Tag? Tag { get; set; }

    }
}
