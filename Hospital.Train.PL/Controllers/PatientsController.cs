using AutoMapper;
using Hospital.Train.BLL;
using Hospital.Train.BLL.Interfaces;
using Hospital.Train.DAL.Models;
using Hospital.Train.PL.ViewModels.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Hospital.Train.PL.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PatientsController
            (
             IUnitOfWork unitOfWork,
             IMapper mapper
            ) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string? name)
        {
            var patient=Enumerable.Empty<Patient>();
            if (string.IsNullOrEmpty(name))
            {
                patient =await _unitOfWork.PatientRepository.GetAllAsync();
            }
            else
            {
                patient=_unitOfWork.PatientRepository.GetByName(name);
            }
            var result = _mapper.Map<IEnumerable<PatientViewModel>>(patient);
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var consultant=await _unitOfWork.ConsultantRepository.GetAllAsync();
            ViewData["consultant"]=consultant;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientViewModel entity)
        {
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Patient>(entity);
                _unitOfWork.PatientRepository.Add(result);
                var count =await _unitOfWork.Complete();
                if (count>0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["consultant"] =await _unitOfWork.ConsultantRepository.GetAllAsync();
            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var patient=await _unitOfWork.PatientRepository.GetByIdAsync(id);
            if (patient == null) 
            {
                return NotFound();
            }
            var result= _mapper.Map<PatientViewModel>(patient);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
          var patient=await _unitOfWork.PatientRepository.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            var result=_mapper.Map<PatientViewModel>(patient);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? ID, PatientViewModel entity)
        {
            if (ModelState.IsValid)
            {
                var result=_mapper.Map<Patient>(entity);
                _unitOfWork.PatientRepository.Delete(result);
                var count=await _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                
            }
            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> Update (int id)
        {
            var consultant =await _unitOfWork.ConsultantRepository.GetAllAsync();
            ViewData["consultant"] = consultant;
            var patient =await _unitOfWork.PatientRepository.GetByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<PatientViewModel>(patient);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int? ID, PatientViewModel entity)
        {
            if (ModelState.IsValid)
            {
                var result = _mapper.Map<Patient>(entity);
                _unitOfWork.PatientRepository.Update(result);
                var count=await _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(entity);
        }
    }
 
}
