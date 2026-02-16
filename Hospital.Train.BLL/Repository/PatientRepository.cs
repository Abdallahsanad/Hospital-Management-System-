using Hospital.Train.BLL.Interfaces;
using Hospital.Train.DAL.Data.Context;
using Hospital.Train.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Train.BLL.Repository
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public PatientRepository(AppDbContext context) : base(context)
        {
        }

       public IEnumerable<Patient> GetByName(string? name)
        {
            return _context.Patients.Where(c => c.Name.ToLower().Contains(name.ToLower())).Include(c => c.Consultant).ToList();
        }
    }
}
