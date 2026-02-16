using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Train.DAL.Models
{
    public class Department : BaseEntity
    {
        public string Code { get; set; }

        public DateTime DateOfCreation { get; set; }
        public ICollection<Consultant>? Consultants { get; set; }
    }
}
