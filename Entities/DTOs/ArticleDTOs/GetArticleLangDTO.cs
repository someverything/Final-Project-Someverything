using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ArticleDTOs
{
    public class GetArticleLangDTO
    {
        public string LangCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
