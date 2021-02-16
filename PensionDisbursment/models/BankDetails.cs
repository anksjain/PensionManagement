using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionDisbursment.Models
{
    public class BankDetails
    {
     
        public int BankDetailId { get; set; }
        public string BankName { get; set; }
        public double AccountNumber { get; set; }
        public string BankType { get; set; }
    }
}
