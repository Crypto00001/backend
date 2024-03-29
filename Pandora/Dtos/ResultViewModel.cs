﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Application.ViewModel
{
    public class Result
    {
        public object Data { get; set; }
        public bool HasError{ get; set; }
        public string ErrorMessage{ get; set; }
        public bool Captcha { get; set; } = false;
    }
}
