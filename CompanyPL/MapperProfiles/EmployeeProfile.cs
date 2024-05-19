using AutoMapper;
using CompanyDAL.Models;
using CompanyPL.ViewModels;

namespace CompanyPL.MapperProfiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
