using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CompanyDAL.Models
{
    public class Department
    {
        
        public int Id { get; set; }//PK Identiny
        [Required]
        public string Code { get; set; }//ref type
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }

        public string  ImageName { get; set; }
        public ICollection<Employee> Employees { get; set; } =new HashSet<Employee>(); //navaigitional prop [many]

    }
}
