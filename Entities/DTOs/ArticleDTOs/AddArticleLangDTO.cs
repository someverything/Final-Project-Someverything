﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.ArticleDTOs
{
    public class AddArticleLangDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LangCode { get; set; }
    }
}