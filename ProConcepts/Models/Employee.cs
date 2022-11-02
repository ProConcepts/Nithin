using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProConcepts.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Employee Name is mandatory.")]
        public string Name { get; set; }

        [Required]
        public int Salary { get; set; }

        [Required]
        public int Department { get; set; }

        
    }
}
