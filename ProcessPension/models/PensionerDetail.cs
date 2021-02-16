using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPension.models
{
    public class PensionerDetail
    {
            public int PensionerId { get; set; }
            public string Name { get; set; }
            public DateTime DOB { get; set; }
            public string PAN { get; set; }
            public double Aadhar { get; set; }
            public double Salary { get; set; }
            public double Allowances { get; set; }
            public string PensionType { get; set; }
            public int BankDetailId { get; set; }
            public BankDetails BankDetails { get; set; }
    }
    public class BankDetails
    {
        
        public int BankDetailId { get; set; }
        public string BankName { get; set; }
        public double AccountNumber { get; set; }
        public string BankType { get; set; }
    }
}
