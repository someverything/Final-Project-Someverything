﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.SubCategoryDTOs
{
    public class CreateSubCatDTO
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
    }
}
