using CompanyBLL.Interfaces;
using CompanyDAL.Contexts;
using CompanyDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyBLL.Repositrios
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
      
            private readonly CompanyDBContext dbContext;
            public GenericRepository(CompanyDBContext dbContext)
            {
                // dbContext = new CompanyDBContext(); //xxxxx
                this.dbContext = dbContext;
            }

            public async Task Add(T item)
            =>await dbContext.Set<T>().AddAsync(item);
            
            // dbContext.Departments.Add(item);
            //return dbContext.SaveChanges();
            

            public void Delete(T item)
            {
                dbContext.Set<T>().Remove(item);
               // return dbContext.SaveChanges();
            }

    

        public async Task<T> Get(int id)
            =>await dbContext.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAll()
        {
            //try
            //{
                if (typeof(T) == typeof(Employee))
                {
                    return  (IEnumerable<T>)await dbContext.Employees.Include(e => e.Department).ToListAsync();
                }
                else
                {
                    return await dbContext.Set<T>().ToListAsync();
                }
           // }
            //catch (Exception)
            //{

            //    return dbContext.Set<T>().ToList();

            //}
        }
        //=> dbContext.Set<T>().ToList();

        public void Update(T Item)
        {
            dbContext.Set<T>().Update(Item);
            //return dbContext.SaveChanges();
        }
    
    }
}
