namespace BlogMainStructure.Business.DTOs.ArticleDTOs
{
    public class ArticleListDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public DateTime PublishDate { get; set; }
        public string TagName { get; set; }
        public string AuthorName { get; set; }
    }
}
