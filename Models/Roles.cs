﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    [Flags]
    public enum Roles
    {
        Read = 1 << 0,
        Create = 1 << 1,
        Update = 1 << 2,
        Delete = 1 << 3
    }
}
