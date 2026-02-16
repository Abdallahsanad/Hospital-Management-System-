using AutoMapper;
using Hospital.Train.PL.ViewModels.Consultant;
using Hospital.Train.PL.ViewModels.Patient;

namespace Hospital.Train.PL.Mapping
{
    public class PatientsProfile : Profile
    {
        public PatientsProfile() 
        {
            // 1. من Entity لـ ViewModel (لعرض البيانات)
            CreateMap<Hospital.Train.DAL.Models.Patient, PatientViewModel>()
                .ForMember(dest => dest.ConsultantName, opt => opt.MapFrom(src => src.Consultant.Name));

            // 2. من ViewModel لـ Entity (لحفظ البيانات)
            CreateMap<PatientViewModel, Hospital.Train.DAL.Models.Patient>()
                .ForMember(dest => dest.Consultant, opt => opt.Ignore()); // أهم سطر: ملمسش الـ Object بتاع القسم خالص
        }
    }
}
