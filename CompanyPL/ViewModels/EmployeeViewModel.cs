using CompanyDAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyPL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Max Length is 50 chars ")]
        [MinLength(5, ErrorMessage = "min Length is 5 chars")]
        public string Name { get; set; }
        [Range(22, 35, ErrorMessage = "Age Must Be In Ranger From 22 to 35")]
        public int? Age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Address Must be Liked 123-Street-City-Country")]

        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        //التاريخ اللي اتعمل فيه الموظف ده 
        // public DateTime CreationDate { get; set; } = DateTime.Now;

        //image
        public string ImageName { get; set; }

        public IFormFile Image {  get; set; } 

        //

        ///////////////////////////////////////////// Relation

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }//fk
        //fk Optional ?=> OnDelete : Restrict
        //fk Required => OnDelete : Cascade //لما تمسح departrment هتمسح اي حد مربوط بيه
        public Department Department { get; set; }//navigitional prop [one  ]
    }
}
