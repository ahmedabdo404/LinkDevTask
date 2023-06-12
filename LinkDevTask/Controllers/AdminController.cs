using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LinkDevTask.Application.Servcices.Interfaces;
using System.Net;
using LinkDevTask.Application.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using LinkDevTask.Application.Consts;

namespace LinkDevTask.WebApp.Controllers
{
    [Authorize(Roles=UserRoles.Admin)]
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public IActionResult DashBoard()
        {
            return View();
        }

        #region Users

        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsers();
            return View(users);
        }

        public async Task<IActionResult> GetUserById(string Id)
        {
            var User = await _adminService.GetUser(Id);
            if (User == default)
            {
                return RedirectToErrorPage(HttpStatusCode.NotFound);
            }
            return View(User);
        }

        #endregion

        #region Roles
        public async Task<IActionResult> GetAllRoles()
        {
            return View(await _adminService.GetAllRoles());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRole(string RoleName)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _adminService.AddRole(RoleName);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(GetAllRoles));
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(nameof(RoleName), err.Description);
                }
            }
            var AllRoles = await _adminService.GetAllRoles();
            return View(nameof(GetAllRoles), AllRoles);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string role)
        {
            IdentityResult result = await _adminService.DeleteRole(role);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors.ToList());
        }

        public async Task<IActionResult> ManageUserRole(string Id)
        {
            var User = await _adminService.GetUser(Id);
            if (User == default)
            {
                return RedirectToErrorPage(HttpStatusCode.NotFound);
            }
            ViewBag.User = User;
            var UserRoles = await _adminService.GetUserRoles(User);
            return View(UserRoles);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRole(string UserId, List<RoleVM> Roles)
        {
            var User = await _adminService.GetUser(UserId);
            if (User == default)
            {
                return RedirectToErrorPage(HttpStatusCode.NotFound);
            }

            ViewBag.User = User;
            var results = await _adminService.ManageUserRoles(User, Roles);

            if (results.All(IR => IR.Succeeded))
            {
                return RedirectToAction(nameof(GetAllUsers));
            }
            foreach (var res in results)
            {
                foreach (var err in res.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            Roles = await _adminService.GetUserRoles(User);
            return View(Roles);
        }
        #endregion
    }
}
