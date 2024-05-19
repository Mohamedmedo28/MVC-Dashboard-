using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBLL.Interfaces
{
    public interface IUnitOfWork :  IDisposable
    {
       IEmployeeRepository EmployeeRepository { get; set;  }
       IDepartmentRepository DepartmentRepository  { get; set; }

        Task<int> Complete();

      
    }
}
