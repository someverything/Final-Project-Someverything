using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.SubCategoryDTOs;
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
        }
    }
}
