using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using testing1.Models;

namespace testing1.ViewModels
{
    public class EmployeeCreateViewModel
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name canot 50 length")]
        [MinLength(2, ErrorMessage = "Name min 2 ")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+$", ErrorMessage = "Invalid Email")]
        [Display(Name = "Office Email")] // ganti label name
        public string Email { get; set; }
        [Required]
        public Dept? Department { get; set; } //AGAR KETIKA MENDAPATKAN VALUE "" atau null tidak error dan validation error akan muncul ketika null
        public List<IFormFile> Photo { get; set; }

    }
}
