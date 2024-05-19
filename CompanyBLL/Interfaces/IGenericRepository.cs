using CompanyDAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBLL.Interfaces
{
    public interface IGenericRepository<T>
    {
        //ASynchronous//
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);

        Task Add(T item);
        void Delete(T item);
        void Update(T item);

        // Synchronous ////
        //IEnumerable<T> GetAll();

        //T Get(int id);

        //void Add(T item);
        //void Delete(T item);
        //void Update(T item);
    }
}
