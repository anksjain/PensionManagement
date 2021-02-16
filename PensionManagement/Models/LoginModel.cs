using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagement.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Aadhar No")]
        [RegularExpression("^[0-9]{12}$", ErrorMessage = "Enter 12 Digit Aadhar Number")]
        public double Aadhar { get; set; }
    }
}
