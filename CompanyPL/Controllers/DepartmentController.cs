using AutoMapper;
using CompanyBLL.Interfaces;
using CompanyBLL.Repositrios;
using CompanyDAL.Models;
using CompanyPL.Helpers;
using CompanyPL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyPL.Controllers
{
	[Authorize]

	public class DepartmentController : Controller
    {
        //اخد interface احسن
       // private readonly IDepartmentRepository departmentRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(/*IDepartmentRepository departmentRepository */ IUnitOfWork unitOfWork,IMapper mapper)
        {//تطبيق dependance Enjection علي departmentRepository
            //هيكلم clr ويشوف اي حد ي امبليمنت انترفيس ده 
            //departmentRepository = new DepartmentRepository();//xxxxxxxx
           //this.departmentRepository = departmentRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult>  Index(string SearchName)
        {//GetAll
            IEnumerable<Department> departments;

            if(string.IsNullOrEmpty(SearchName))
                departments =await unitOfWork.DepartmentRepository.GetAll();
            else
                departments =  unitOfWork.DepartmentRepository.GetSearchBYName(SearchName);

            var MapperDEP = mapper.Map<IEnumerable<Department> ,IEnumerable<DepartmentViewModel>>(departments);
            return View(MapperDEP);
        }
        [HttpGet]
        public IActionResult Create()
        { 
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult>  Create(DepartmentViewModel departmentVM)
        {
             if (ModelState.IsValid)//server side validation
            {
                departmentVM.ImageName = DocumentSettings.UploadImage(departmentVM.Image, "Images");
                var MapperDEP = mapper.Map<DepartmentViewModel, Department>(departmentVM);

                await unitOfWork.DepartmentRepository.Add(MapperDEP);
                int result = await unitOfWork.Complete();
                if (result > 0)
                {
                    TempData["message"] = "Department is created! ";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }
        
     
        public async Task<IActionResult> Details(int? id , string viewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var Department = await unitOfWork.DepartmentRepository.Get(id.Value);

            if(Department is null)
                return NotFound();

            var MapperDEP = mapper.Map<Department , DepartmentViewModel>(Department);
            return View( viewName,MapperDEP);
        }
        [HttpGet]
        public async Task<IActionResult>  Edit(int? id)
        {
            return await Details(id, "Edit");
            //if (id is null)
            //    return BadRequest();
            //var Department = departmentRepository.Get(id.Value);

            //if (Department is null)
            //    return NotFound();
            //return View(Department);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]//route only
        public async Task<IActionResult> Edit([FromRoute]int? id,DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MapperDEP = mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    unitOfWork.DepartmentRepository.Update(MapperDEP);
                  await  unitOfWork.Complete();
                    return RedirectToAction(nameof (Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    
                }
            }
            return View(departmentVM);
                
        }
        [HttpGet]
        public async Task<IActionResult>  Delete(int? id)
        {
            if(id is null)
                return BadRequest();
             
            var department =await unitOfWork.DepartmentRepository.Get(id.Value);
            if(department is null)
                return NotFound();

            var MapperDEP = mapper.Map<Department, DepartmentViewModel>(department);

            return View(MapperDEP);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int id , DepartmentViewModel departmentVM)
        {
            if(id != departmentVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MapperDEP = mapper.Map<DepartmentViewModel , Department>(departmentVM);

                    if (MapperDEP.ImageName != null)
                    {
                        DocumentSettings.DeleteFile( MapperDEP.ImageName ,"Images");
                    }
                    unitOfWork.DepartmentRepository.Delete(MapperDEP);
                   await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }

            }
            return View(departmentVM);


        }
    }
}
