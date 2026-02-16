using AutoMapper;
using Hospital.Train.PL.ViewModels.Consultant;

namespace Hospital.Train.PL.Mapping.Consultant
{
    public class ConsultantProfile : Profile
    {
        public ConsultantProfile() 
        {
            //CreateMap<Hospital.Train.DAL.Models.Consultant, ConsultantViewModel>()
            //    .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.WorkFor.Name))
            //.ReverseMap();

            // 1. من Entity لـ ViewModel (لعرض البيانات)
            CreateMap<Hospital.Train.DAL.Models.Consultant, ConsultantViewModel>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.WorkFor.Name));

            // 2. من ViewModel لـ Entity (لحفظ البيانات)
            CreateMap<ConsultantViewModel, Hospital.Train.DAL.Models.Consultant>()
                .ForMember(dest => dest.WorkFor, opt => opt.Ignore()); // أهم سطر: ملمسش الـ Object بتاع القسم خالص
        
    }
    }
}
