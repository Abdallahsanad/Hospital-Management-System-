using AutoMapper;
using Hospital.Train.PL.ViewModels.Department;
using Hospital.Train.DAL.Models; 

namespace Hospital.Train.PL.Mapping.Department
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile() 
        {
             CreateMap<Hospital.Train.DAL.Models.Department, CreateDepartmentViewModel>().ReverseMap();
        }
    }
}
