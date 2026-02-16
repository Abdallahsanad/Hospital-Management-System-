using Hospital.Train.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hospital.Train.BLL.Interfaces
{
    public interface IConsultantRepository : IGenericRepository<Consultant>
    {
        IEnumerable<Consultant> GetByName(string? name);
    }
}
