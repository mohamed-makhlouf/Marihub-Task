using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Marihub_Task.Models
{
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }
        public int? Age { get; set; }
        [Required(ErrorMessage ="Plz Enter your User Name:")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match")]
        public string ConfirmPassword { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public Manager()
        {
            Departments = new HashSet<Department>();
        }

    }
}
