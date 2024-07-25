using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ArticlePicture : Entities.Common.File
    {
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
