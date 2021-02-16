using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPension.models
{
    public class PensionerInputDetails
    {
        public string PensionerName { get; set; }
        public DateTime PensionerDOB { get; set; }
        public string PensionerPAN { get; set; }
        public double PensionAmount { get; set; }
        public string PensionType { get; set; }
        public double Aadhar { get; set; }
    }
}
