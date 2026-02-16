using Hospital.Train.BLL.Interfaces;
using Hospital.Train.DAL.Data.Context;
using Hospital.Train.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Train.BLL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Consultant))
            {
                return await _context.Consultants.Include(c => c.WorkFor).Cast<T>().ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }



       public async Task<T> GetByIdAsync(int? id)
        {
            return await _context.Set<T>().FindAsync(id);
        }




       public void Add(T entity)
        {
            _context.Add(entity);
            
        }

       public void Update(T entity)
        {
            _context.Update(entity);
            
        }

        public void Delete( T entity)
        {
            _context.Remove(entity);
            
        }

        

        
    }
}
