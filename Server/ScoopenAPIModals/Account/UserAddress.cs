﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoopenAPIModals.Account
{
    public class UserAddress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LineNumber1 { get; set; }
        public string LineNumber2 { get; set; }
        public string Village { get; set; }
        public string Taluka { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
    }
}
