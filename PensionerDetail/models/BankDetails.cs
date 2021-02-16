using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetail.Models
{
    public class BankDetails
    {
        [Key]
        public int BankDetailId { get; set; }
        public string BankName { get; set; }
        public double AccountNumber { get; set; }
        public string BankType { get; set; }
    }
}
