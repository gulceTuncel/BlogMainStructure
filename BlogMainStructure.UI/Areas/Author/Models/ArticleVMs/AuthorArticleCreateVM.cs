using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlogMainStructure.UI.Areas.Author.Models.ArticleVMs
{
    public class AuthorArticleCreateVM
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public SelectList? Tags { get; set; }
        public IFormFile? NewImage { get; set; }
        public Guid? TagId { get; set; }
        public Guid? AuthorId { get; set; }
    }
}
