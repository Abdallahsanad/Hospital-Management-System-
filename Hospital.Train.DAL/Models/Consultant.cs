using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Train.DAL.Models
{
    public class Consultant : BaseEntity
    {
        public DateTime DateOfBirth { get; set; }

        [NotMapped] 
        public int Age
        {
            get
            {
                return DateTime.Now.Year - DateOfBirth.Year;
            }
        }


        public string Address { get; set; }



        public decimal Salary { get; set; }

        public string Email { get; set; }


 
        public string Phone { get; set; }


        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }


        public DateTime HiringDate { get; set; }
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public int? WorkForId { get; set; }
        public Department? WorkFor { get; set; }
        public string? ImageName { get; set; }
    }
}
