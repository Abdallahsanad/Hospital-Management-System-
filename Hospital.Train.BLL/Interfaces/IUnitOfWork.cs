using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Train.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository DepartmentRepository { get; }
        public IConsultantRepository ConsultantRepository { get;}
        public IMedicineRepository MedicineRepository { get;}
        public IPatientRepository PatientRepository { get;}

        public Task<int> Complete();
    }
}
