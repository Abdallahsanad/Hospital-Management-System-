using System.ComponentModel.DataAnnotations;

namespace Hospital.Train.PL.ViewModels.Patient
{
    public class PatientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }

        public string? ConsultantName { get; set; }
    }
}
