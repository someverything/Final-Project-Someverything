using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ArticleDTOs
{
    public class AddArticleDTO
    {
        public string Title { get; set; }
        public string LangCode { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public ICollection<ArticlePhoto> ArticlePhotos { get; set; }
    }
}
