﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PensionManagement.Models
{
    public class Pension
    {
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string PAN { get; set; }
        public string PensionType { get; set; }
        public double PensionAmount { get; set; }
    }
}
