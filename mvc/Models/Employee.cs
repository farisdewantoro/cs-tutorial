using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace testing1.Models
{

    public class Employee
    {
       
        public int Id{get;set;}
        [Required]
        [MaxLength(50,ErrorMessage="Name canot 50 length")]
        [MinLength(2,ErrorMessage="Name min 2 ")]
        public string Name{get;set;}
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+$",ErrorMessage="Invalid Email")]
        [Display(Name = "Office Email")] // ganti label name
        public string Email {get;set;}
        [Required]
        public Dept? Department{get;set;} //AGAR KETIKA MENDAPATKAN VALUE "" atau null tidak error dan validation error akan muncul ketika null
        public string PhotoPath {get;set;}
        
    }
}