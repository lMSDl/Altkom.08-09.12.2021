using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize]
    public class LoginController : Controller
    {
        private IService<User> _service;

        public LoginController(IService<User> service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string username, string password, string returnUrl)
        {
            var user = _service.Read().Where(x => x.Username == username).SingleOrDefault(x => x.Password == password);
            if(user == null)
            {
                ModelState.AddModelError(nameof(username), "Invalid credentials");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            claims.AddRange(user.Role.ToString().Split(',').Select(x => new Claim(ClaimTypes.Role, x.Trim())));

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Redirect(returnUrl);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
