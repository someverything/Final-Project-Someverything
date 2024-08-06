using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ArticleTagDTOs
{
    public class ArticleTagDTO
    {
        public Guid ArticleId { get; set; }
        public Guid TagId { get; set; }
    }
}
