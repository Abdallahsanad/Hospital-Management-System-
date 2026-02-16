using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Train.BLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int? id);
      

        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);


    }
}
