using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Services;

namespace RazorPages.Pages.Users
{
    [Authorize(Roles = "Create, Update")]
    public class AddOrEditModel : PageModel
    {

        public IService<User> Service { get; }
        [BindProperty]
        public User SelectedUser { get; set; }

        public AddOrEditModel(IService<User> service)
        {
            Service = service;
        }

        public IActionResult OnGet(int id)
        {
            SelectedUser = Service.Read().SingleOrDefault(x => x.Id == id);
            if (SelectedUser == null)
            {
                SelectedUser = new User();
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if(SelectedUser.Id == 0)
                Service.Create(SelectedUser);
            else
                Service.Update(SelectedUser.Id, SelectedUser);

            return RedirectToPage("./Index");
        }
    }
}
