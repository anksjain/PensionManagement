using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagement.Models
{
    public class PensionerDetails
    {
        [Key]
        public int PensionerId { get; set; }

        public string Name { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime DOB { get; set; }
        [Display(Name = "PAN Number")]
        public string PAN { get; set; }
        [Display(Name = "Aadhar Number")]
        public double Aadhar { get; set; }
        public double Salary { get; set; }
        public double Allowances { get; set; }
        [Display(Name = "Type of Pension")]
        public string PensionType { get; set; }

        [ForeignKey("BankDetails")]
        public int BankDetailId { get; set; }
        public  virtual BankDetails BankDetails { get; set; }

    }
}
