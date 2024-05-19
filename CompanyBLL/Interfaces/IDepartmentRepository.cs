using CompanyDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyBLL.Interfaces
{
    public interface IDepartmentRepository:IGenericRepository<Department>
    {
        public IQueryable<Department> GetSearchBYName(string name);

        //IEnumerable<Department> GetAll();

        //Department Get(int id);
        
        //int Add(Department department);
        //int Delete(Department department);
        //int Update(Department department);

    }
}
