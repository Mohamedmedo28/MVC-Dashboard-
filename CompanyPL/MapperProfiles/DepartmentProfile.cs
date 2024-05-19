using AutoMapper;
using CompanyDAL.Models;
using CompanyPL.ViewModels;

namespace CompanyPL.MapperProfiles
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel , Department>().ReverseMap();
        }
    }
}
