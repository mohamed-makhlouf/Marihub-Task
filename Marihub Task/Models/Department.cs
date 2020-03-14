using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marihub_Task.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        public string  DeptName { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public Department()
        {
            Employees = new HashSet<Employee>();
        }
    }
}
