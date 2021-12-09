using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services;

namespace RazorPages.Pages.Users
{
    public class IndexModel : PageModel
    {
        public IService<User> Service { get; }

        public IndexModel(IService<User> service)
        {
            Service = service;
        }

        public void OnGet()
        {
        }
    }
}
