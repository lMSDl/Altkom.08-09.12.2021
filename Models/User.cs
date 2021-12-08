using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User : Entity
    {
        [DisplayName("Login")]
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
