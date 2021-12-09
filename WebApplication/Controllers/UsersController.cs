using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class UsersController : Controller
    {
        private IService<User> _service;

        public UsersController(IService<User> service)
        {
            _service = service;
        }


        [Authorize(Roles = nameof(Roles.Read))]
        public IActionResult Index()
        {
            return View(_service.Read());
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = nameof(Roles.Read))]
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


        [Authorize(Roles = "Read, Delete")] //OR     // \
                                                     //   => AND
        //[Authorize(Roles = nameof(Roles.Read))]    // /
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var item = _service.Read(id.Value);
            if ( item == null)
                return NotFound();

            return View(item);
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        [Authorize(Roles = nameof(Roles.Delete))]
        public IActionResult DeleteUser(int id)
        {
            _service.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = nameof(Roles.Update))]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var item = _service.Read(id.Value);
            if (item == null)
                return NotFound();

            return View(item);
        }

        [HttpPost]
        [Authorize(Roles = nameof(Roles.Update))]
        public IActionResult EditUser(int id, [Bind("Password", "Role", "Username")]User user)
        {
            if (!ModelState.IsValid)
            {
                user.Id = id;
                return View("Edit", user);
            }

            var item = _service.Read(id);
            if (item.Password == user.Password)
            {
                ModelState.AddModelError(nameof(user), "Hasło nie może być takie jak ostatnio");
                return View("Edit", user);
            }

            user.Username = item.Username;
            _service.Update(id, user);

            return RedirectToAction(nameof(Index));
        }
    }
}
