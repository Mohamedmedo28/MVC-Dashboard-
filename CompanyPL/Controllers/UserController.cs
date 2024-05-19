using CompanyDAL.Models;
using CompanyPL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AutoMapper;
using System;
using CompanyBLL.Repositrios;

namespace CompanyPL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IMapper mapper;

        public UserController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager
            ,IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mapper = mapper;
        }
      
        public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                var users = await userManager.Users.Select(U => new UserViewModel()
                {
                    Id = U.Id,
                    FName = U.FName,
                    LName = U.LName,
                    Email = U.Email,
                    PhoneNumber = U.PhoneNumber,
                    Roles = userManager.GetRolesAsync(U).Result
                }).ToListAsync();

                return View(users);
            }
            else
            {
                var user = await userManager.FindByEmailAsync(email);

                var MapperUser = new UserViewModel()
                {
                    Id = user.Id,
                    FName = user.FName,
                    LName = user.LName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Roles = userManager.GetRolesAsync(user).Result
                };
                return View(new List<UserViewModel>() { MapperUser  } );


            }
            
        }

        public async Task<IActionResult> Details(string Id ,string viewName="Details" )
        {
            if (Id is null)
                return BadRequest();
            var user = await userManager.FindByIdAsync(Id);

            if(user is null)
                return NotFound();

            var MappedUser =  mapper.Map< ApplicationUser,UserViewModel>(user);
            return View(viewName , MappedUser);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]string id , UserViewModel UserVM)
        {
            if(id != UserVM.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    //var user = mapper.Map<UserViewModel, ApplicationUser>(UserVM);//deatache => ع user ملوش اي علاقه ب الداتا بيز
                   //manual mapping => same mapper
                    var user = await userManager.FindByIdAsync (id);
                    user.FName = UserVM.FName;
                    user.LName = UserVM.LName;
                    user.PhoneNumber = UserVM.PhoneNumber;
                    //user.Email = UserVM.Email;
                    //user.SecurityStamp = Guid.NewGuid().ToString();

                    await userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));   

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message); 
                    throw;
                }
            }
            return View(UserVM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id is null)
                return BadRequest();

           var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();

            var MapperDEP = mapper.Map<ApplicationUser, UserViewModel>(user);

            return View(MapperDEP);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]string? id, UserViewModel userVM)
        {
            if(id != userVM.Id)
                return BadRequest();

            if(ModelState.IsValid)
            {

                try
                {
                    var user = await userManager.FindByIdAsync(id);
                    //user.FName = userVM.FName;
                    //user.LName = userVM.LName;
                    //user.PhoneNumber = userVM.PhoneNumber;

                    await userManager.DeleteAsync(user);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    return View(userVM);
                }

            }
            return View(userVM);
        }


    }
}
