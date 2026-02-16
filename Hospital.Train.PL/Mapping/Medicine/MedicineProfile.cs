using AutoMapper;
using Hospital.Train.PL.ViewModels.Medicine;

namespace Hospital.Train.PL.Mapping.Medicine
{
    public class MedicineProfile : Profile
    {
        public MedicineProfile() 
        {
            CreateMap<Hospital.Train.DAL.Models.Medicine, MidicineViewModel>().ReverseMap();
        }
    }
}
