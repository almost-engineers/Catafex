﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService.Models
{
    public class Administrador : Usuario
    {
        string codigo { get; set; }
        string nivelExp { get; set; }
    }
}