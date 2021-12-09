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
    public class DeleteModel : PageModel
    {
        public IService<User> Service { get; }
        public User SelectedUser { get; set; }

        public DeleteModel(IService<User> service)
        {
            Service = service;
        }

        public IActionResult OnGet(int id)
        {
            SelectedUser = Service.Read().SingleOrDefault(x => x.Id == id);
            if (SelectedUser == null)
                return NotFound();
            return Page();
        }

        //Dzia³a domyœlnie na POST, a z pomoc¹ SupportsGet te¿ na GET
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public IActionResult OnPost()
        {
            Service.Delete(Id);

            return RedirectToPage("./Index");
        }

    }
}
