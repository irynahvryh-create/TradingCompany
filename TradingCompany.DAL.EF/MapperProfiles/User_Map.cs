using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingCompany.DAL.EF.Models;
using TradingCompany.DTO;
using System.Linq;


namespace TradingCompany.DAL.EF.MapperProfiles
{
    public class User_Map : Profile
    {
        public User_Map()
        {
            // DTO -> EF
            CreateMap<DTO.User, Models.User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.Salt, opt => opt.Ignore())
                .ForMember(dest => dest.UserPrivileges, opt => opt.MapFrom(u =>
                    u.Privileges.Select(p => new UserPrivilege
                    {
                        UserId = u.UserID,
                        PrivilegeId = p.PrivilegeID,
                        RowInsertTime = DateTime.UtcNow
                    })
                ));

            // EF -> DTO
            CreateMap<Models.User, DTO.User>()
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(u => u.UserId))
                .ForMember(dest => dest.Privileges, opt => opt.MapFrom(u =>
                    u.UserPrivileges.Select(up => up.Privilege)
                ));
        }
    }
}
