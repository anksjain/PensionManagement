using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetail.Models
{
    public class PensionerDetails
    {
        [Key]
        public int PensionerId { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string PAN { get; set; }
        public double Aadhar { get; set; }
        public double Salary { get; set; }
        public double Allowances { get; set; }
        public string PensionType { get; set; }

        [ForeignKey("BankDetails")]
        public int BankDetailId { get; set; }
        public  virtual BankDetails BankDetails { get; set; }

    }
}
