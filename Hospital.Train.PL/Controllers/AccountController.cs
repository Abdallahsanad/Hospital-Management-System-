using Hospital.Train.DAL.Models;
using Hospital.Train.PL.Helper;
using Hospital.Train.PL.ViewModels.AccountController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities; 
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Train.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManger;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController
            (
              UserManager<ApplicationUser> userManager,
              SignInManager<ApplicationUser> signInManager
            )
        {
           _userManager=userManager;
            _signInManger=signInManager;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user =await _userManager.FindByNameAsync(entity.UserName);
                    if (user is null)
                    {
                        user =await _userManager.FindByEmailAsync(entity.Email);
                        if (user is null)
                        {
                            user=new ApplicationUser()
                                {
                                    UserName=entity.UserName,
                                    Email=entity.Email,
                                    FirstName=entity.FirstName,
                                    LastName=entity.LastName,
                                    IsAgree=entity.IsAgree
                                };
                            var result =await _userManager.CreateAsync(user, entity.Password);
                            if (result.Succeeded)
                            {
                                return RedirectToAction(nameof(SignIn));
                            }
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                        ModelState.AddModelError(string.Empty, "Email Is Already exists");
                    }
                    ModelState.AddModelError(string.Empty, "UserName Is Already exists");
                }
            
            }
            catch(Exception ex) 
            { 
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View(entity);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByEmailAsync(entity.Email);
                    if (user is not null)
                    {
                        var flag =await _userManager.CheckPasswordAsync(user, entity.Password);
                        if (flag)
                        {
                            var result=await _signInManger.PasswordSignInAsync(user, entity.Password,entity.RememberMe,false);
                            if (result.Succeeded)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                           
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Login");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
            }
            return View(entity);
        }

        
        public new async Task<IActionResult> SignOut()
        {
           await _signInManger.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPassword(SendResetPassword entity)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user=await _userManager.FindByEmailAsync(entity.Email);
                    if (user is not null)
                    {
                        //Create Token
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                        //create password
                        var url = Url.Action("ResetPassword", "Account", new { email = user.Email, token = encodedToken }, Request.Scheme);

                        //create Email
                        var email = new Email()
                        {
                            To = entity.To,
                            Subject = "Reset Password",
                            Body = url
                        };

                        //Send Email
                        EmailSettings.SendEmail(email);
                        return RedirectToAction(nameof(CHeckYourInbox));
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Operation ,Try Again Later");  
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError (string.Empty,ex.Message);
            }
            return View(entity);
        }
        [HttpGet]
        public IActionResult CHeckYourInbox()
        {
            return View(); 
        }

        //[HttpGet]
        //public IActionResult ResetPassword(string email,string token)
        //{
        //    TempData["email"]=email;
        //    TempData["token"]=token;
        //    return View();
        //}

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token)) return BadRequest();
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var model = new ResetPasswordViewMode
            {
                Email = email,
                Token = decodedToken
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewMode model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  

                    var user=await _userManager.FindByEmailAsync(model.Email);
                    if (user is not null)
                    { 
                        var result=await _userManager.ResetPasswordAsync(user,model.Token,model.Password);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(SignIn));
                        }
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Operation Please Try Again");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
            }
            return View(model);
        }


        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
