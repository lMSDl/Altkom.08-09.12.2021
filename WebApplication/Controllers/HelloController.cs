using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication.Controllers
{
    public class HelloController : Controller
    {
        public string StartPage(string idd)
        {
            return $"Hello {idd}!";
        }

        public string Encode()
        {
            return HttpUtility.HtmlEncode(@"<b>Pewne nibezpieczne znaki: $ < > \ </b>");
        }
    }
}
