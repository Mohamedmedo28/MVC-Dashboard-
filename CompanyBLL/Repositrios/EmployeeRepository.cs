 using CompanyBLL.Interfaces;
using CompanyDAL.Contexts;
using CompanyDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyBLL.Repositrios
{
    public class EmployeeRepository: GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly CompanyDBContext dBContext;

        public EmployeeRepository(CompanyDBContext dBContext) : base(dBContext)
        {
            this.dBContext = dBContext;
        }

        public IQueryable<Employee> GetEmployeesByName(string Name)
        {
            return dBContext.Employees.Where(e => e.Name.ToLower().Contains(Name.ToLower()));
        }

        IQueryable<Employee> IEmployeeRepository.GetEmployeesByAddress(string address)
        {
            return dBContext.Employees.Where(e => e.Address == address);

        }
        //    private readonly CompanyDBContext dbContext;
        //    public EmployeeRepository(CompanyDBContext dbContext)
        //    {
        //        // dbContext = new CompanyDBContext(); //xxxxx
        //        this.dbContext = dbContext;
        //    }

        //    public int Add(Employee employee)
        //    {
        //        dbContext.Employees.Add(employee);
        //        return dbContext.SaveChanges();
        //    }

        //    public int Delete(Employee employee)
        //    {
        //        dbContext.Employees.Remove(employee);
        //        return dbContext.SaveChanges();
        //    }

        //    public Employee Get(int id)
        //    => dbContext.Find<Employee>(id);

        //    public IEnumerable<Employee> GetAll()
        //    => dbContext.Employees.ToList();

        //    public int Update(Employee employee)
        //    {
        //        dbContext.Employees.Update(employee);
        //        return dbContext.SaveChanges();
        //    }
    }
}
