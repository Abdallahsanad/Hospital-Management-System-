using AutoMapper;
using Hospital.Train.BLL;
using Hospital.Train.BLL.Interfaces;
using Hospital.Train.BLL.Repository;
using Hospital.Train.DAL.Models;
using Hospital.Train.PL.ViewModels.Consultant;
using Hospital.Train.PL.ViewModels.Medicine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hospital.Train.PL.Controllers
{
    [Authorize]
    public class MedicineController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _mapper;

        public MedicineController
            (
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _UnitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string? name)
        {
            var medicine =Enumerable.Empty<Medicine>();
            if (string.IsNullOrEmpty(name))
            {
               medicine =await _UnitOfWork.MedicineRepository.GetAllAsync();
            }
            else
            {
                medicine=_UnitOfWork.MedicineRepository.GetByName(name);
            }
            var result = _mapper.Map<IEnumerable<MidicineViewModel>>(medicine);
            return View(result);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MidicineViewModel entity)
        {
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Medicine>(entity);
                _UnitOfWork.MedicineRepository.Add(result);
                var count =await _UnitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var med =await _UnitOfWork.MedicineRepository.GetByIdAsync(id);
            if (med==null)
            {
                return NotFound();
            }
            var result=_mapper.Map< MidicineViewModel >(med);
            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var med =await _UnitOfWork.MedicineRepository.GetByIdAsync(id);
            if (med == null) return NotFound();
            var result=_mapper.Map<MidicineViewModel>(med);
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? ID,MidicineViewModel entity)
        {
            var result = _mapper.Map<Medicine>(entity);
             _UnitOfWork.MedicineRepository.Delete(result);
            var count =await _UnitOfWork.Complete();
            if (count > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(entity);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var med =await _UnitOfWork.MedicineRepository.GetByIdAsync(id);
            var result = _mapper.Map<MidicineViewModel>(med);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int? ID,MidicineViewModel entity)
        {
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Medicine>(entity);
                _UnitOfWork.MedicineRepository.Update(result);
                var count =await _UnitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(entity);

        }


    }
}
