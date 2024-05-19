using CompanyBLL.Interfaces;
using CompanyDAL.Contexts;
using CompanyDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyBLL.Repositrios
{
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository 
    {
        private readonly CompanyDBContext dBContext;
        public DepartmentRepository(CompanyDBContext dBContext):base(dBContext) 
        {
            this.dBContext = dBContext;
        }

        public CompanyDBContext DBContext { get; }

        public IQueryable<Department> GetSearchBYName(string name)
        {
            return dBContext.Departments.Where(e => e.Name.ToLower().Contains(name.ToLower())); 
        }
        //    private readonly CompanyDBContext dbContext;
        //    public DepartmentRepository(CompanyDBContext dbContext)
        //    {
        //        // dbContext = new CompanyDBContext(); //xxxxx
        //        this.dbContext = dbContext;
        //    }

        //    public int Add(Department department)
        //    {
        //       dbContext.Departments.Add(department);
        //        return dbContext.SaveChanges();
        //    }

        //    public int Delete(Department department)
        //    {
        //        dbContext.Departments.Remove(department);
        //        return dbContext.SaveChanges();
        //    }

        //    public Department Get(int id)
        //    => dbContext.Find<Department>(id);

        //    public IEnumerable<Department> GetAll()
        //    => dbContext.Departments.ToList();

        //    public int Update(Department department)
        //    {
        //        dbContext.Departments.Update(department);
        //        return dbContext.SaveChanges();
        //    }
    }
}
