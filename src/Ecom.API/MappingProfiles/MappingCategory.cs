using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.core.Entities;

namespace Ecom.API.MappingProfiles
{
    public class MappingCategory: Profile
    {
        public MappingCategory()
        {
            CreateMap<Category, ListingCategoryDto>().ReverseMap();
            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();
        }
    }
}
