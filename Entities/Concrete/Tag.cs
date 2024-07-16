using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ArticleTag> ArticleTags { get; set; }
    }
}
