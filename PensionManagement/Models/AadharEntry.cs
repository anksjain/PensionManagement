using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagement.Models
{
    public class AadharEntry
    {
        [Required]
        [Display(Name = "Aadhar No")]
        [RegularExpression("^[0-9]{12}$", ErrorMessage = "Enter 12 Digit Aadhar Number")]
        public double AadharNo { get; set; }
       
    }
}
