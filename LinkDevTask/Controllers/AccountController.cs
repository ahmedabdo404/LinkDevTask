using LinkDevTask.Application.Consts;
using LinkDevTask.Application.Servcices.Interfaces;
using LinkDevTask.Application.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LinkDevTask.WebApp.Controllers
{
    public class AccountController : BaseController
    {
        #region ctor
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        #endregion

        public IActionResult Register()
        {
            if (_accountService.IsSignedIn(User))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM NewUser)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _accountService.CreateUser(NewUser);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }
            return View(NewUser);
        }

        #region Login & Logout

        public IActionResult Login(string ReturnUrl = General.DefaultUrl)
        {
            if (_accountService.IsSignedIn(User))
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            ViewBag.RedirectUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInVM LoggedUser, string ReturnUrl = General.DefaultUrl)
        {
            ViewBag.RedirectUrl = ReturnUrl;

            if (ModelState.IsValid)
            {
                var result = await _accountService.Login(LoggedUser);
                if (result.Succeeded)
                {
                    return LocalRedirect(ReturnUrl);
                }

                string ErrorMessage =  "Incorrect UserName Or Passwword";
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            return View(LoggedUser);
        }

        public async Task<IActionResult> Logout(LogInVM LoggedUser)
        {
            await _accountService.Logout();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion
    }
}
