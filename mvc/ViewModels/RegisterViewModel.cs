using System;
using System.ComponentModel.DataAnnotations;


namespace testing1.ViewModels
{
    public class RegisterViewModel
    {
        
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+]+@[a-zA-Z0-9]+\.[a-zA-Z0-9]+$", ErrorMessage = "Invalid Email")]

        public string Email { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name canot 50 length")]
        [MinLength(2, ErrorMessage = "Name min 2 ")]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [DataType(DataType.Password)]
        [Display(Name="Confirm Password")]
        [Compare("Password", ErrorMessage="Password and confirm pass doesnt match")]
        public string ConfirmPassword {get;set;}
    }
}
