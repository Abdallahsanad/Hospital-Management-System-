using Hospital.Train.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace Hospital.Train.PL.ViewModels.Department
{
    public class CreateDepartmentViewModel
    {
        //[Required(ErrorMessage = "Code is required")]
        //public string Code { get; set; }

        //public DateTime DateOfCreation { get; set; }
        //public ICollection<Consultant>? Consultants { get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }

        public DateTime DateOfCreation { get; set; }
        //public ICollection<Consultant>? Consultants { get; set; }
    }
}
