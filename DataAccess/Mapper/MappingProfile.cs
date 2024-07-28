using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.SubCategoryDTOs;

namespace DataAccess.Mapper
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
