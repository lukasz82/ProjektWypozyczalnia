using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Movies.Models;
using Movies.Models.AccountViewModels;

namespace Movies.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminPanelController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Data.ApplicationDbContext _context;

        public AdminPanelController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (await _userManager.IsInRoleAsync(user, "Admin") != true)
            {
                if (user != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        ViewBag.UserDeleteError = "usunięto użytkownika" + " : " + user.UserName;
                        //return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.UserDeleteError = "nie udało się usunąć użytkownika" + " : " + user.UserName;
                        //return RedirectToAction("Index");
                    }
                }
            }
            else
            {
                ViewBag.UserDeleteError = "nie możesz usunąć administratora";
            }
            return View("Index", _userManager.Users);
        }
    }
}