using CompanyDAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace CompanyPL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }//PK Identiny
        [Required(ErrorMessage = "Code Is Required !!")]
        public string Code { get; set; }//ref type
        [Required(ErrorMessage = "Name Is Required !!")]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z]{5,10}$",
            ErrorMessage = "Name is max 10")]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        //
        public string ImageName { get; set; }
        public IFormFile Image { get; set; }

        //

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>(); //navaigitional prop [many]
    }
}
