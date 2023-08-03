﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaDeAdquisiciones
{
    public class EndPoint
    {
        public string EndPointGateway { get; set; }
        public string EndPointFileManager { get; set; }
        public string EndPointCapGastosCore { get; set; }
    }
}
