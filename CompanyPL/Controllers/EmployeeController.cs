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

	public class EmployeeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        //private readonly IEmployeeRepository employeeRepository;
        //private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;

        public EmployeeController(
           IUnitOfWork unitOfWork 
            /*IEmployeeRepository employeeRepository ,IDepartmentRepository departmentRepository*/ ,IMapper mapper)//ask clr for creation object from employee repostriy
        {
            this.unitOfWork = unitOfWork;
            //this.employeeRepository = employeeRepository;
            //this.departmentRepository = departmentRepository;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            //test data binding
            //1 //ViewData["message"] = "hello from view data";
            //2 // ViewBag.message = "hello from view data";
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchValue))
                employees = await unitOfWork.EmployeeRepository.GetAll();
            else
                employees = unitOfWork.EmployeeRepository.GetEmployeesByName(SearchValue);
            //
            var MapperEmp = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(MapperEmp);
        }

        public async Task<IActionResult> Search(string SearchValue)
        {
            //test data binding
            //1 //ViewData["message"] = "hello from view data";
            //2 // ViewBag.message = "hello from view data";
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchValue))
                employees = await unitOfWork.EmployeeRepository.GetAll();
            else
                employees = unitOfWork.EmployeeRepository.GetEmployeesByName(SearchValue);
            //
            var MapperEmp = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return PartialView("employeeTablePartialView", MapperEmp);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.departrment =await unitOfWork.DepartmentRepository.GetAll();
         
            return View();
        }
        [HttpPost]
        //public IActionResult Create(Employee employee)
        public  async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
           // try
           // {
                if (ModelState.IsValid)
                {
                    var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    MappedEmp.ImageName = DocumentSettings.UploadImage(employeeVM.Image , "Images");
                    await unitOfWork.EmployeeRepository.Add(MappedEmp);
                   int result =await  unitOfWork.Complete();
                    if (result > 0)
                    {
                        TempData["message"] = "Employee is created! ";
                    }
                    return RedirectToAction(nameof(Index));

                }
                return View(employeeVM);
           // }
            //catch (Exception)
            //{

            //    return RedirectToAction(nameof(Index));

            //}


        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee =await unitOfWork.EmployeeRepository.Get(id.Value);
            

            if (employee is null)
                return NotFound();
            //reverse Employee 
            var MapperEmp = mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(MapperEmp);
        }
        [HttpGet]
        public async Task<IActionResult>  Edit(int? id)
        {
            ViewBag.departrment =await unitOfWork.DepartmentRepository.GetAll();


            if (id is null )
                return BadRequest();
            var Employee =  await unitOfWork.EmployeeRepository.Get(id.Value);
            if(Employee is null)
                return NotFound();

            var MapperEMP = mapper.Map<Employee,EmployeeViewModel>(Employee);
            return View(MapperEMP);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Edit([FromRoute]int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MapperEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    unitOfWork.EmployeeRepository.Update(MapperEmp);
                   await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(employeeVM);
        }


        [HttpGet]
        public async Task<IActionResult>  Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee =await unitOfWork.EmployeeRepository.Get(id.Value);
            if (employee is null)
                return NotFound();

            var MapperEmp = mapper.Map<Employee , EmployeeViewModel>(employee);
            return View(MapperEmp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel employeeVM)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MapperEMP = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    //hidden image
                    if (MapperEMP.ImageName != null)
                    {
                        DocumentSettings.DeleteFile( MapperEMP.ImageName,"Images");
                    }

                    unitOfWork.EmployeeRepository.Delete(MapperEMP);
                  await  unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }

            }
            return View(employeeVM);


        }


    }
}
