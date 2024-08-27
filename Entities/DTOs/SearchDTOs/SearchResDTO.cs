using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.SearchDTOs
{
    public class SearchResDTO
    {
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string Username { get; set; }
        public string ArticleTitle { get; set; }
    }
}
