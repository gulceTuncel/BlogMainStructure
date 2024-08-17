namespace BlogMainStructure.Business.DTOs.ArticleDTOs
{
    public class ArticleCreateDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; } = 0;
        public Guid? TagId { get; set; }
        public Guid AuthorId { get; set; }
        public byte[]? Image { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
    }
}
