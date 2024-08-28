using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ArticleDTOs
{
    public class UpdateArticleDTO
    {
        public bool IsActive { get; set; }
        public ICollection<UpdateArticleLangDTO> UpdateArticleLangDTOs { get; set; }
        public ICollection<UpdateArticlePhotoDTO> UpdateArticlePhotoDTOs { get; set; }
        public ICollection<Guid> SubCatId { get; set; }
        public ICollection<Guid> TagId { get; set; }

    }
}
