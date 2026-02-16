using System.ComponentModel.DataAnnotations;

namespace Hospital.Train.PL.ViewModels.AccountController
{
    public class ResetPasswordViewMode
    {
        public string Email { get; set; }
        public string Token { get; set; }
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword Is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "ConfirmPassword Is Not Match Password")]
        public string ConfirmPassword { get; set; }
    }
}
