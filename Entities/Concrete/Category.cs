using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Category : BaseEntity
    {
        public ICollection<CategoryLang> CategoryLangs { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
