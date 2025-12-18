using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using TradingCompany.DTO;

namespace TradingCompany.WebApp.App.MappingProfiles
{
    public class CategoryListItemProfile : Profile
    {
        public CategoryListItemProfile()
        {
            CreateMap<Category, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(c => c.CategoryID)) // ⚡ CategoryID, як у DTO
                .ForMember(dest => dest.Text, opt => opt.MapFrom(c => c.Name));
        }
    }
}
