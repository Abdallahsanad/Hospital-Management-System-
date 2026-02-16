using Hospital.Train.BLL.Interfaces;
using Hospital.Train.BLL.Repository;
using Hospital.Train.DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Train.BLL
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;
        private readonly IMedicineRepository _medicineRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IConsultantRepository _consultantRepository;
        private readonly IPatientRepository _petientRepository;

        public UnitOfWork(AppDbContext context) 
        {
            _departmentRepository=new DepartmentRepository(context);
            _consultantRepository=new ConsultantRepository(context);
            _medicineRepository=new MedicineRepository(context);
            _petientRepository=new PatientRepository(context);
            _context=context;   
        }
        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public IConsultantRepository ConsultantRepository => _consultantRepository;

       public IMedicineRepository MedicineRepository => _medicineRepository;

       public IPatientRepository PatientRepository =>_petientRepository ;

       


        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        
    }
}
