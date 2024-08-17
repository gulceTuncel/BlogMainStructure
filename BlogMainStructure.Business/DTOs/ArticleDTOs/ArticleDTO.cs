using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMainStructure.Business.DTOs.ArticleDTOs
{
    public class ArticleDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int ViewCount { get; set; }
        public DateTime PublishDate { get; set; }
        public string AuthorName { get; set; }
        public string TagName { get; set; }

        public byte[]? Image { get; set; }
        public byte[]? AuthorImage { get; set; }
    }
}
