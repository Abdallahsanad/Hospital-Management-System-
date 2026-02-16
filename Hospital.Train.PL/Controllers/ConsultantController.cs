using AutoMapper;
using Hospital.Train.BLL.Interfaces;
using Hospital.Train.DAL.Models;
using Hospital.Train.PL.Helper;
using Hospital.Train.PL.ViewModels.Consultant;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Hospital.Train.PL.Controllers
{
    [Authorize]
    public class ConsultantController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ConsultantController(
            IUnitOfWork unitOfWork,
            IMapper mapper
            )

        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string search)
        {

            var cons = Enumerable.Empty<Consultant>();
            if (string.IsNullOrEmpty(search))
            {
                cons = await _unitOfWork.ConsultantRepository.GetAllAsync();
            }
            else
            {
                cons = _unitOfWork.ConsultantRepository.GetByName(search);
            }
            var result = _mapper.Map<IEnumerable<ConsultantViewModel>>(cons);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var dept = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["department"] = dept;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ConsultantViewModel entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.Image is not null)
                {
                    entity.ImageName = DocumentSettings.Upload(entity.Image, "images");
                }
                var result = _mapper.Map<Consultant>(entity);
                _unitOfWork.ConsultantRepository.Add(result);
                var count = await _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            ViewData["department"] = await _unitOfWork.DepartmentRepository.GetAllAsync();

            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            var dept = await _unitOfWork.DepartmentRepository.GetAllAsync();
            ViewData["department"] = dept;
            var cons = await _unitOfWork.ConsultantRepository.GetByIdAsync(id);

            if (cons == null) return NotFound();
            var result = _mapper.Map<ConsultantViewModel>(cons);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int? id, ConsultantViewModel entity)
        {
            if (ModelState.IsValid)
            {
                if (entity.ImageName is not null)
                {
                    DocumentSettings.Delete(entity.ImageName, "images");
                }
                if (entity.Image is not null)
                {
                    entity.ImageName = DocumentSettings.Upload(entity.Image, "images");
                }
                var result = _mapper.Map<Consultant>(entity);
                _unitOfWork.ConsultantRepository.Update(result);
                var count = await _unitOfWork.Complete();
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }


            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var cons = await _unitOfWork.ConsultantRepository.GetByIdAsync(id);
            if (cons == null) return NotFound();
            var result = _mapper.Map<ConsultantViewModel>(cons);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            var cons = await _unitOfWork.ConsultantRepository.GetByIdAsync(id);
            if (cons == null) return NotFound();
            var result = _mapper.Map<ConsultantViewModel>(cons);
            return View(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int? id, ConsultantViewModel entity)
        {
            var result = _mapper.Map<Consultant>(entity);
            _unitOfWork.ConsultantRepository.Delete(result);
            var count = await _unitOfWork.Complete();
            if (count > 0)
            {
                if (entity.ImageName is not null)
                {
                    DocumentSettings.Delete(entity.ImageName, "images");
                }
                return RedirectToAction(nameof(Index));
            }

            return View(entity);
        }

    }
}
