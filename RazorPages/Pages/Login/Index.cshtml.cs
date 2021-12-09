using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services;

namespace RazorPages.Pages.Login
{
    public class IndexModel : PageModel
    {
        public IService<User> Service { get; }

        public IndexModel(IService<User> service)
        {
            Service = service;
        }

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = Service.Read().Where(x => x.Username == Username).SingleOrDefault(x => x.Password == Password);
            if (user == null)
            {
                ModelState.AddModelError(nameof(Username), "Invalid credentials");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            claims.AddRange(user.Role.ToString().Split(',').Select(x => new Claim(ClaimTypes.Role, x.Trim())));

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            if (!Url.IsLocalUrl(ReturnUrl))
                ReturnUrl = Url.Content("/");
            return Redirect(ReturnUrl);
        }
    }
}
