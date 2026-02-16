using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Train.DAL.Models
{
    public class Patient : BaseEntity
    {
        [DataType(DataType.Date)]

        public DateOnly DateOfBirth { get; set; }


        [ForeignKey("Consultant")]
        public int ConsultantID { get; set; }
        public Consultant? Consultant { get; set; }
    }
}
