using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPension.models
{
    public class ProcesPensionOutput
    {
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string PAN { get; set; }
        public string PensionType { get; set; }
        public double PensionAmount { get; set; }
    }
}
