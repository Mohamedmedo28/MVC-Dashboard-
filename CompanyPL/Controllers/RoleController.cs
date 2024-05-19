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

namespace CompanyPL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public RoleController(RoleManager<IdentityRole> roleManager , IMapper mapper)
        {
            this.roleManager = roleManager;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var Roles =await roleManager.Roles.Select(r => new RoleViewModel()
                {
                    Id = r.Id,
                    RoleName = r.Name,
                }).ToListAsync();
                return View(Roles);
            }
            else
            {
                var Roles = await roleManager.FindByNameAsync(name);
                var MappedRole = new RoleViewModel()
                {
                    Id = Roles.Id,
                    RoleName = Roles.Name,
                };
                return View(new List<RoleViewModel>() { MappedRole});

                 
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleVm)
        {
            if (ModelState.IsValid)
            {
                var MappedRole = mapper.Map<RoleViewModel, IdentityRole>(roleVm);
                await roleManager.CreateAsync(MappedRole);
                return RedirectToAction(nameof(Index));

            }
            return View(roleVm);
        }

        public async Task<IActionResult> Details(string id ,string ViewName="Details")
        {
                if (id == null)
                 return BadRequest();

            var Roles = await roleManager.FindByIdAsync(id);
            if (Roles == null)
                return NotFound();

            var MappedRole = mapper.Map<IdentityRole, RoleViewModel>(Roles);
            return View(ViewName, MappedRole);
                    
        
        }

        public async Task<IActionResult> Edit(string Id )
        {
            return await Details(Id ,"Edit");  
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]string id, RoleViewModel roleVM)
        {
            if(id != roleVM.Id)
                return BadRequest();
            if(ModelState.IsValid)
            {
                //var Roles = mapper.Map<RoleViewModel , IdentityRole>(roleVM);
                var role =await roleManager.FindByIdAsync(id);
                role.Name = roleVM.RoleName;
                  await roleManager.UpdateAsync(role);
                return RedirectToAction(nameof(Index));
            }
            return View(roleVM);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]string id ,RoleViewModel roleVM)
        {
            if(id != roleVM.Id)
                return BadRequest();
            try
            {

                if (ModelState.IsValid)
                {
                    var Role = await roleManager.FindByIdAsync(id);

                    await roleManager.DeleteAsync(Role);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty , ex.Message);
                return View(roleVM);

            }
            return View(roleVM) ;
        }

    }
}
