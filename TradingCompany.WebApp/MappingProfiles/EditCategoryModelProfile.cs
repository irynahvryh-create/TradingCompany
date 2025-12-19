using AutoMapper;
using TradingCompany.DTO;
using TradingCompany.WebApp.Models;

namespace TradingCompany.WebApp.MappingProfiles
{
    public class EditCategoryModelProfile : Profile
    {
        public EditCategoryModelProfile()
        {
            // DTO → ViewModel 
            CreateMap<Category, EditCategoryModel>();

            // ViewModel → DTO 
            CreateMap<EditCategoryModel, Category>();
        }
    }
}
