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
    public class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository
    {
        public MedicineRepository(AppDbContext context) : base(context)
        {
        }

       public IEnumerable<Medicine> GetByName(string? name)
        {
            return _context.Medicines.Where(c => c.Name.ToLower().Contains(name.ToLower())).ToList();

        }
    }
}
