using AutoMapper;
using TradingCompany.DTO;
using TradingCompany.WebApp.Models;

namespace TradingCompany.WebApp.MappingProfiles
{
    public class EditCategoryModelProfile : Profile
    {
        public EditCategoryModelProfile()
        {
            // DTO → ViewModel (для показу у формі)
            CreateMap<Category, EditCategoryModel>();

            // ViewModel → DTO (після сабміту форми)
            CreateMap<EditCategoryModel, Category>();
        }
    }
}
