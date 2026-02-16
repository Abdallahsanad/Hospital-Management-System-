using AutoMapper;
using Hospital.Train.BLL.Interfaces;
using Hospital.Train.DAL.Models;
using Hospital.Train.PL.ViewModels.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital.Train.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentController(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _unitOfWork= unitOfWork;
            _mapper = mapper;
        }





        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments=await _unitOfWork.DepartmentRepository.GetAllAsync();
            var result = _mapper.Map<IEnumerable<CreateDepartmentViewModel>>(departments);
            return View(result);
        }





        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentViewModel entity)
        {
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Department>(entity);
                _unitOfWork.DepartmentRepository.Add(result);
                var count =await _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
           
            return View(entity);
        }


        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var department =await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department == null) return NotFound();

            return View(department);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Department entity)
        {
            //var result=_mapper.Map<Department>(entity);
             _unitOfWork.DepartmentRepository.Delete(entity);
            var count =await _unitOfWork.Complete();
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }

        [HttpGet]

        public async Task<IActionResult> Details(int? id)
        {
            

                var department =await _unitOfWork.DepartmentRepository.GetByIdAsync(id);

                if (department == null) return NotFound();
                
            
            var result = _mapper.Map<CreateDepartmentViewModel>(department);

            return View(result);
        }


        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var department =await _unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department == null) return NotFound();
            var result=_mapper.Map<CreateDepartmentViewModel>(department);

            return View(result);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CreateDepartmentViewModel entity)
        {
            if (ModelState.IsValid)
            {
                var result=_mapper.Map<Department>(entity);
                 _unitOfWork.DepartmentRepository.Update(result);
                var count =await _unitOfWork.Complete();
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                
            }
            return View(entity);
        }

    }
}
