﻿using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.ArticleDTOs;
using Entities.DTOs.SubCategoryDTOs;
using Entities.DTOs.TagDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SubCategory, GetSubCatDTO>();
            CreateMap<SubCategory, CreateSubCatDTO>();
            CreateMap<SubCategory, UpdateSubCatDTO>();

            CreateMap<Tag, CreateTagDTO>();
            CreateMap<Tag, UpdateTagDTO>();
            CreateMap<Tag, GetTagDTO>();
            CreateMap<Tag, UpdateTagDTO>();

            CreateMap<Article, UpdateArticleDTO>();
            CreateMap<Article, GetArticleDTO>();
            CreateMap<Article, AddArticleDTO>();
            CreateMap<Article, AddArticleLangDTO>();
            CreateMap<Article, AddArticlePhotoDTO>();
            CreateMap<Article, UpdateArticlePhotoDTO>();
            CreateMap<Article, UpdateArticleLangDTO>();
        }
    }
}
