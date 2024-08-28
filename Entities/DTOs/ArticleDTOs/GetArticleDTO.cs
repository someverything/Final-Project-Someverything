using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ArticleDTOs
{
    public class GetArticleDTO
    {

        public string Title { get; set; }
        public string Description { get; set; }
        public string LangCode { get; set; }
        public ICollection<string> SubCategoryName { get; set; }
        public ICollection<string> TagName { get; set; }
        public long Views { get; set; }
        public ICollection<ArtSubCat> ArtSubCats { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
        public string CreatedBy { get; set; }
        public ICollection<string> Photo { get; set; }
    }
}
