using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class UsersController : Controller
    {
        private IService<User> _service;

        public UsersController(IService<User> service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.Read());
        }
    }
}
