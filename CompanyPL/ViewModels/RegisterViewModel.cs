using System.ComponentModel.DataAnnotations;

namespace CompanyPL.ViewModels
{
	public class RegisterViewModel
{
		[Required(ErrorMessage = "First Name Is Required")]
		public string FName { get; set; }
		[Required(ErrorMessage = "Last Name Is Required")]

		public string LName { get; set; }

		//[Required(ErrorMessage = "UserName Is Required")]
		// public string UserName { get; set; }

		[Required(ErrorMessage = "Email Is Required")]
		[EmailAddress]
		public string Email { get; set; }
        [Required(ErrorMessage ="Password Is Required")]
		[DataType(DataType.Password)]//*********
		public string Password { get; set; }

		[Required(ErrorMessage = "Password Is Required")]
		[DataType(DataType.Password)]//*********
		[Compare("Password",ErrorMessage ="Confirm Password Does not Match Password")]
		public string ConfirmPassword { get; set; }
		public bool IsAgree { get; set; }
    }
}
