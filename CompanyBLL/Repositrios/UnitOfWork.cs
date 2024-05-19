using CompanyBLL.Interfaces;
using CompanyDAL.Contexts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBLL.Repositrios
{
    public class UnitOfWork : IUnitOfWork 
    {
        public IEmployeeRepository EmployeeRepository { get; set ; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public CompanyDBContext dBContext { get; }

        public UnitOfWork(CompanyDBContext dBContext)
        {
            this.dBContext = dBContext;
            EmployeeRepository = new EmployeeRepository(dBContext);
            DepartmentRepository = new DepartmentRepository(dBContext);
        }

        public async Task<int> Complete()
        {
           return await dBContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            dBContext.Dispose();
        }
    }
}
