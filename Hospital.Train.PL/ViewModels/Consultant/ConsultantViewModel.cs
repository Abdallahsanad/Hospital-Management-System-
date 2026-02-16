using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Train.PL.ViewModels.Consultant
{
    public class ConsultantViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }



        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }



        [Required(ErrorMessage = "Salary is required")]
        public decimal Salary { get; set; }



        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }


        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }


        public bool IsActive { get; set; }
       
        public DateTime HiringDate { get; set; }
        public int? WorkForId { get; set; }
        [Display(Name = "Department")]
        public string? DepartmentName { get; set; }
        public IFormFile? Image {  get; set; }
        public string? ImageName {  get; set; }
    }
}
