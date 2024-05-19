using AutoMapper;
using CompanyPL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace CompanyPL.MapperProfiles
{
    public class RoleProfile:Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViewModel , IdentityRole>()
                .ForMember(m=>m.Name,o=>o.MapFrom(s=>s.RoleName)).ReverseMap();
        }
    }
}
