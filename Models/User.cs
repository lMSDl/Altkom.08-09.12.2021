﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User : Entity
    {
        [DisplayName("Login")]
        //[Required]
        public string Username { get; set; }
        //[MinLength(8, ErrorMessage = "Za którkie!")]
        public string Password { get; set; }

        public Roles Role { get; set; }

    }
}
