using AutoMapper;
using CompanyDAL.Models;
using CompanyPL.ViewModels;

namespace CompanyPL.MapperProfiles
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser , UserViewModel>().ReverseMap();
        }
    }
}
