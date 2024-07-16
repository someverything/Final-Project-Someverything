using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Article : BaseEntity
    {
        public ICollection<ArticleLang> ArticleLangs { get; set; }
        public bool IsActive { get; set; }
        public long Views { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

    }
}
