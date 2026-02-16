using System.ComponentModel.DataAnnotations;

namespace Hospital.Train.PL.ViewModels.AccountController
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="UserName Is Required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "FirstName Is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName Is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress (ErrorMessage ="Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)] 
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword Is Required")]
        [DataType (DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "ConfirmPassword Is Not Match Password")]
        public string ConfirmPassword { get; set; }
        public bool IsAgree { get; set; }





    }
}
