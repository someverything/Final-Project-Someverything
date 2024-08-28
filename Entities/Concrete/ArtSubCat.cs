using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ArtSubCat : BaseEntity
    {
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
        public Guid SubCatId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
