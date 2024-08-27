using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.SearchDTOs
{
    public class SearchCriteriaDTO
    {
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Username { get; set; }
        public string ArticleTitle { get; set; }
        public string LangCode { get; set; }
    }
}
