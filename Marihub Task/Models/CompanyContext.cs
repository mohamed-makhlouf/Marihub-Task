using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marihub_Task.Models
{
    public class CompanyContext:IdentityDbContext
    {
        public CompanyContext(DbContextOptions options ):base(options)
        {

        }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
    }
}
