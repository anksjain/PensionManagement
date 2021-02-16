using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagement.Models
{
    public class PensionInput
    {
        [Required]
        [Display(Name = "Name")]
        
        public string PensionerName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        [Required]
        public DateTime PensionerDOB { get; set; }
        [Required]
        [Display(Name = "PAN")]
        public string PensionerPAN { get; set; }
        [Required]
        [Display(Name = "Type of Pension")]
        public string PensionType { get; set; }
        [Required]
        [Display(Name = "Aadhar No")]
        [RegularExpression("^[0-9]{12}$" ,ErrorMessage="Enter 12 Digit Aadhar Number")]
        public double Aadhar { get; set; }
    }
}
