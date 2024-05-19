using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompanyDAL.Models
{
	public class ApplicationUser:IdentityUser
	{
		[Required(ErrorMessage ="First Name Is Required")]
		public string FName { get; set; }
		[Required(ErrorMessage = "Last Name Is Required")]


		public string LName { get; set; }

		public bool IsAgree { get; set; }
	}
}
