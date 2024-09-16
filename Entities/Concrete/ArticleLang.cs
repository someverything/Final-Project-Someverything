using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ArticleLang : BaseEntity
    {
        public string Title { get; set; }
        public string LangCode { get; set; }
        public string Description { get; set; }
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
