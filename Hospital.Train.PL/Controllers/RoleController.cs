using Hospital.Train.DAL.Models;
using Hospital.Train.PL.ViewModels.Role;
using Hospital.Train.PL.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hospital.Train.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RoleController
            (
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager

            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string search)
        {
            try
            {
                var roleQuery = _roleManager.Roles;
                if (!string.IsNullOrEmpty(search))
                {
                    roleQuery = roleQuery.Where(R => R.Name.ToLower().Contains(search.ToLower()));
                }

                var rolesFromDb = await roleQuery.ToListAsync();
                var rolesViewModelList = rolesFromDb.Select(R => new RoleViewModel
                {
                    Id = R.Id,
                    RoleName = R.Name
                }).ToList();

                return View(rolesViewModelList);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(new List<RoleViewModel>());
            }
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole { Name = model.RoleName };
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded) return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }



        public async Task<IActionResult> Details(string? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();
            var roleFromDb = await _roleManager.FindByIdAsync(id);
            if (roleFromDb is null) return NotFound();

            var roleViewModel = new RoleViewModel { Id = roleFromDb.Id, RoleName = roleFromDb.Name };
            return View(ViewName, roleViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id) => await Details(id, "Edit");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var roleFromDb = await _roleManager.FindByIdAsync(id);
                    if (roleFromDb is null) return NotFound();

                    roleFromDb.Name = model.RoleName;
                    var result = await _roleManager.UpdateAsync(roleFromDb);
                    if (result.Succeeded) return RedirectToAction(nameof(Index));

                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id) => await Details(id, "Delete");

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string? id, RoleViewModel model)
        {
            try
            {
                if (id is null) return BadRequest();
                var roleFromDb = await _roleManager.FindByIdAsync(id);
                if (roleFromDb is null) return NotFound();

                var result = await _roleManager.DeleteAsync(roleFromDb);
                if (result.Succeeded) return RedirectToAction(nameof(Index));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            catch (Exception ex) { ModelState.AddModelError(string.Empty, ex.Message); }
            return View(model);
        }




        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role is null) return NotFound();


            ViewData["RoleId"] = RoleId;
            var UsersInRole = new List<UsersInRolesViewModel>();
            var Users = await _userManager.Users.ToListAsync();

            foreach (var user in Users)
            {
                var userInRole = new UsersInRolesViewModel()
                {
                    Id = user.Id,
                    RoleName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }

                else
                {
                    userInRole.IsSelected = false;
                }
                UsersInRole.Add(userInRole);
            }
            return View(UsersInRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrRemoveUsers(string RoleId, List<UsersInRolesViewModel> users)
        {
            var role=await _roleManager.FindByIdAsync(RoleId);
            if (role is null) return NotFound();
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var user in users)
                    {
                        var appuser =await _userManager.FindByIdAsync(user.Id);
                        if (appuser is not null)
                        {
                            if (user.IsSelected && !await _userManager.IsInRoleAsync(appuser,role.Name))
                            {
                               await _userManager.AddToRoleAsync(appuser,role.Name);
                            }
                            else if (!user.IsSelected && await _userManager.IsInRoleAsync(appuser, role.Name))
                            {
                               await _userManager.RemoveFromRoleAsync(appuser,role.Name);
                            }
                        }
                        
                    }
                    return RedirectToAction(nameof(Edit), new { id = role.Id });
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

            }
            return View(users);
        }


        
    }
}