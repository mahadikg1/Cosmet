﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoopenAPIModals.Notifications
{
    public class LoginDetails
    {
        public string From { get; set; }
        public string To { get; set; }

        public string TemplateName { get; set; }
        public string VAR1 { get; set; }
        public string VAR2 { get; set; }
        public string VAR3 { get; set; }
    }
}
