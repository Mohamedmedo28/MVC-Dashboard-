using CompanyDAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyDAL.Contexts
{
    public class CompanyDBContext:IdentityDbContext<ApplicationUser>
    {//IdentityDbContext => (IdentityUser) || (IdentityUser)

        public CompanyDBContext(DbContextOptions<CompanyDBContext> options) : base(options)
        {

        }
        
        

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=>    optionsBuilder.UseSqlServer("Server=.;database=CompanyMVC;Trusted_Connection=True");

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
