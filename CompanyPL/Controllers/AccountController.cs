using Abp.Net.Mail;
using Abp.Reflection;
using CompanyDAL.Models;
using CompanyPL.Helpers;
using CompanyPL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Threading.Tasks;

namespace CompanyPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMailSettings mailSettings;
        private readonly UserManager<ApplicationUser> userManager;
		
		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager ,IMailSettings mailSettings)
		{
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mailSettings = mailSettings;
        }

        #region Register


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {


            if (ModelState.IsValid)//Server Side validation
            {
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.Email.Split("@")[0],// الجزء اللي قبل @ لاف الايميل
                    FName = model.FName,
                    LName = model.LName,
                    IsAgree = model.IsAgree,

                }; 

                var Result = await userManager.CreateAsync(user, model.Password);
                //create => ناقصه implmentation
                if (Result.Succeeded)
                    return RedirectToAction(nameof(Login));

                foreach (var error in Result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

            }
            return View(model);
        }

		#endregion


		#region Login
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					var flag = await userManager.CheckPasswordAsync(user, model.Password);
					if (flag)
					{
						var Result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
						if (Result.Succeeded)
							return RedirectToAction("Index", "Home");


					}
					ModelState.AddModelError(string.Empty, "is not Valid");

				}
				ModelState.AddModelError(string.Empty, "is not Valid");

			}
			return View(model);
		}
        #endregion

        #region SignOut
        public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
		#endregion
		#region ForgetPassword
		[HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
		[HttpPost]

		public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if(ModelState.IsValid)//server side validation 
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {//account/Resetpassword //token safe //يتبعت مع الايميبل مره واحده بس
				 //var PasswordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email }, "https", "localhost:44347");
				 //https:\\localhost:44341\Account\ResetPassword\mido@email.com@token=wef54r5fer2f121er2dqwdweqfe232
				 //
                    var Token = await userManager.GeneratePasswordResetTokenAsync(user);//Token valid for one time
					var PasswordResetLink = Url.Action("ResetPassword", "Account", new { email = model.Email , token = Token }, Request.Scheme);

					var email = new Email()
                    {
                        Subject = "Reset Password",
                        To = user.Email ,
                        Body = PasswordResetLink
                        

					};//helper Function // static
					
					//EmailSettings.SendEmail(email);

                    mailSettings.SendMail(email);
                    return RedirectToAction(nameof(CheckYourInbox));
                }
                ModelState.AddModelError(string.Empty, "Email is not Valid");
            }
            return View(model);

        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }
		#endregion

		#region ResetPassword
		[HttpGet]
		public IActionResult ResetPassword(string email , string token)
		{
            TempData["token"] = token;
			TempData["email"] = email;
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
            if (ModelState.IsValid)
            {
                var token = TempData["token"] as string;
                var email = TempData["email"] as string;
                var user = await userManager.FindByEmailAsync(email);

             var Result =await userManager.ResetPasswordAsync(user , token , model.NewPassword);

                if (Result.Succeeded)
                    return RedirectToAction(nameof(Login));
                foreach (var error in Result.Errors) 
                    ModelState.AddModelError(string.Empty , error.Description);
                    
                
            }
			return View(model);
		}

		#endregion

	}
}
