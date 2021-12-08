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
            return Search(null, null);
        }

        public IActionResult Search(string phrase, Roles? roles)
        {
            var users = _service.Read();
            if(!string.IsNullOrWhiteSpace(phrase))
            {
                users = users.Where(x => x.Password.Contains(phrase, StringComparison.InvariantCultureIgnoreCase) || x.Username.Contains(phrase, StringComparison.InvariantCultureIgnoreCase));
            }
            if (roles.HasValue)
                users = users.Where(x => x.Role.HasFlag(roles.Value));

            return View(nameof(Index), users);
        }
    }
}
