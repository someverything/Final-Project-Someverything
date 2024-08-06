using Entities.DTOs.ArticleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.TagDTOs
{
    public class GetTagDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
