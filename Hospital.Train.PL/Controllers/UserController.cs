using Hospital.Train.DAL.Models;
using Hospital.Train.PL.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Train.PL.Controllers
{
    [Authorize (Roles ="Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string search)
        {
            try
            {
                var usersQuery = _userManager.Users;

                if (!string.IsNullOrEmpty(search))
                {
                    usersQuery = usersQuery.Where(u => u.Email.ToLower().Contains(search.ToLower()));
                }
                var usersFromDb = await usersQuery.ToListAsync();
                var userViewModels = new List<UserViewModel>();

                foreach (var u in usersFromDb)
                {
                    userViewModels.Add(new UserViewModel()
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        Roles = await _userManager.GetRolesAsync(u) // استخدام await صحيح هنا
                    });
                }

                return View(userViewModels);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(new List<UserViewModel>());
            }
        }

        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            try
            {
                if (id is null)
                    return BadRequest();

                var userfromdb = await _userManager.FindByIdAsync(id);
                if (userfromdb is null)
                {
                    return NotFound();
                }

                var userViewModel = new UserViewModel()
                {
                    Id = userfromdb.Id,
                    FirstName = userfromdb.FirstName,
                    LastName = userfromdb.LastName,
                    Email = userfromdb.Email,
                    Roles = await _userManager.GetRolesAsync(userfromdb)
                };

                return View(ViewName, userViewModel); // تم تغيير User الكبيرة إلى userViewModel الصغيرة
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel model)
        {
            try
            {
                if (id != model.Id)
                    return BadRequest();

                if (ModelState.IsValid)
                {
                    var userFromDb = await _userManager.FindByIdAsync(id);
                    if (userFromDb is null)
                    {
                        return NotFound();
                    }

                    userFromDb.FirstName = model.FirstName;
                    userFromDb.LastName = model.LastName;
                    userFromDb.Email = model.Email;
                    userFromDb.UserName = model.Email.Split('@')[0]; // تحديث الـ UserName ليتوافق مع الإيميل الجديد

                    var result = await _userManager.UpdateAsync(userFromDb);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, UserViewModel model)
        {
            try
            {
                if (id is null) return BadRequest();

                var userFromDb = await _userManager.FindByIdAsync(id);
                if (userFromDb is null) return NotFound();

                var result = await _userManager.DeleteAsync(userFromDb);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View(model);
        }
    }
}