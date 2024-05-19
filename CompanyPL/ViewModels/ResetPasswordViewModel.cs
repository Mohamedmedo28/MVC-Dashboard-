using System.ComponentModel.DataAnnotations;

namespace CompanyPL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "New Password Is Required")]
        [DataType(DataType.Password)]//*********
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmPassword Is Required")]
        [DataType(DataType.Password)]//*********
        [Compare("NewPassword", ErrorMessage = "Confirm Password Does not Match Password")]
        public string ConfirmPassword { get; set; }
		// حل اخر احسن بتاع tempdata
		//public string Token { get; set; }
		//public string Email { get; set; }
	}
}
