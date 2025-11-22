using AutoMapper;
using TradingCompany.DTO;
namespace TradingCompany.DAL.EF.MapperProfiles
{
    public class Privilege_Map : Profile
    {

        public Privilege_Map() {
            CreateMap<Privilege, Models.Privilege>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToString()))
            .ForMember(dest => dest.UserPrivileges, opt => opt.Ignore());
            CreateMap<Models.Privilege, Privilege>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Enum.Parse<PrivilegeType>(src.Name)));
        }
    }
}
