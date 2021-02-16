using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagement.Models
{
    public class BankDetails
    {
        [Key]
        public int BankDetailId { get; set; }
        [Display(Name = "Name of the Bank")]
        public string BankName { get; set; }
        [Display(Name = "Account Number")]
        public double AccountNumber { get; set; }
        [Display(Name = "Type of Bank")]
        public string BankType { get; set; }
    }
}
