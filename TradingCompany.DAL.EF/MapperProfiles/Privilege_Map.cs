using AutoMapper;
using TradingCompany.DTO;

namespace TradingCompany.DAL.EF.MapperProfiles
{
    public class Privilege_Map : Profile
    {
        public Privilege_Map()
        {
            // DTO → EF
            CreateMap<Privilege, Models.Privilege>()
                .ForMember(dest => dest.PrivilegeId, opt => opt.MapFrom(src => src.PrivilegeID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToString()))
                .ForMember(dest => dest.UserPrivileges, opt => opt.Ignore()); // ігноруємо навігаційне поле

            // EF → DTO
            CreateMap<Models.Privilege, Privilege>()
                .ForMember(dest => dest.PrivilegeID, opt => opt.MapFrom(src => src.PrivilegeId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => Enum.Parse<PrivilegeType>(src.Name)));
        }
    }
}
