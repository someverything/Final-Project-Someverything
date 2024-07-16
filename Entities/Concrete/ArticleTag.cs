using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ArticleTag : BaseEntity
    {
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
