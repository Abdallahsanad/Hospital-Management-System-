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
    public class ConsultantRepository : GenericRepository<Consultant>, IConsultantRepository
    {
        public ConsultantRepository(AppDbContext context) : base(context)
        {
        }

        public IEnumerable<Consultant> GetByName(string?  name)
        {
          return _context.Consultants.Where(c => c.Name.ToLower().Contains(name.ToLower())).Include(c => c.WorkFor).ToList();
        }

        
    }
}
