using CompanyDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyBLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {

        public IQueryable<Employee> GetEmployeesByAddress(string address);

        public IQueryable<Employee> GetEmployeesByName(string Name);

        //ctrl f =>select all
        //IEnumerable<Employee> GetAll();

        //Employee Get(int id);

        //int Add(Employee employee);
        //int Delete(Employee employee);
        //int Update(Employee employee);
    }
}
