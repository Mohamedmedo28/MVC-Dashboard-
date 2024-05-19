using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CompanyDAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        //[MinLength(5 , ErrorMessage = "min Length is 5 chars")]
        public string Name { get; set; }
        //[Range(22,35 ,ErrorMessage ="Age Must Be In Ranger From 22 to 35")]
        public int? Age { get; set; }
       // [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$" , 
       //     ErrorMessage ="Address Must be Liked 123-Street-City-Country")]

        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool  IsActive { get; set; }
        //[EmailAddress]
        public string  Email { get; set; }
        //[Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        //
        //Image File
        public string ImageName { get; set; }

        //
        //التاريخ اللي اتعمل فيه الموظف ده 
        public DateTime CreationDate { get; set; }=DateTime.Now;
      
        ///////////////////////////////////////////// Relation
      
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }//fk
        //fk Optional ?=> OnDelete : Restrict
        //fk Required => OnDelete : Cascade //لما تمسح departrment هتمسح اي حد مربوط بيه
        public Department Department { get; set; }//navigitional prop [one  ]

    }
}
