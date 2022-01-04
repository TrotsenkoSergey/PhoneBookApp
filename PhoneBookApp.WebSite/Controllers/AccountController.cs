using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhoneBookApp.WebSite.Models;
using Microsoft.AspNetCore.Identity;
using PhoneBookApp.WebSite.AuthModels;
using Microsoft.Extensions.Logging;
using DataAccess.ContextEF;
using System.Security.Claims;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;

namespace PhoneBookApp.WebSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _context;

        public AccountController(UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View(new UserLogin());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(
                    userName: model.LoginProp,
                    password: model.Password,
                    isPersistent: model.IsRemembered,
                    lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                    return RedirectToAction("Index", "PhoneBooks");
                }

            }

            ModelState.AddModelError("", "Пользователь не найден");
            return View(model);
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegistration());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.LoginProp, Email = model.EmailAddress };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Проверяем наличие ролей. Если запись первая - создаем роли
                    // и определяем пурвую запись ролью админа.
                    if (!_context.Roles.Any())
                    {
                        _context.Roles.AddRange(new IdentityRole("Admin"), new IdentityRole("User"));
                        await _context.SaveChangesAsync();

                        var x = _context.Roles.Where(x => x.Name == "Admin").FirstOrDefault().Id;
                        var y = _context.Users.Where(x => x.UserName == user.UserName).FirstOrDefault().Id;

                        var identityUserRole = new IdentityUserRole<string>() { RoleId = x, UserId = y };
                        _context.UserRoles.Add(identityUserRole);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        var x = _context.Roles.Where(x => x.Name == "User").FirstOrDefault().Id;
                        var y = _context.Users.Where(x => x.UserName == user.UserName).FirstOrDefault().Id;

                        var identityUserRole = new IdentityUserRole<string>() { RoleId = x, UserId = y };
                        _context.UserRoles.Add(identityUserRole);
                        await _context.SaveChangesAsync();
                    }

                    await _signInManager.SignInAsync(user, model.IsRemembered);

                    return RedirectToAction("Index", "PhoneBooks");
                }
                else//иначе
                {
                    foreach (var identityError in result.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                }
            }

            return View(model);
        }


        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "PhoneBooks");
        }

    }



}
