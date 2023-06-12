using LinkDevTask.Application.Consts;
using LinkDevTask.Application.Helpers;
using LinkDevTask.Domain.Models;
using LinkDevTask.Application.Servcices.Interfaces;
using LinkDevTask.Application.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevTask.Application.Servcices.Implementations
{
    public class AccountService : IAccountService
    {
        #region ctor

        readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;
        public AccountService(UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #endregion

        public async Task<IdentityResult> CreateUser(RegisterVM NewUserVM)
        {
            var (userByEmail, userByName) = await GetUser(NewUserVM.Email, NewUserVM.UserName);

            if (userByEmail is null && userByName is null)
            {
                var user = Mapper.MapTo<User>(NewUserVM);
                await _userManager.AddToRoleAsync(user, UserRoles.User);
                var createResult = await _userManager.CreateAsync(user, NewUserVM.Password);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return IdentityResult.Success;
                }
            }

            var UserErrors = new List<IdentityError>();
            if (userByEmail is not null)
            {
                UserErrors.Add(new IdentityError()
                {
                    Code = nameof(NewUserVM.Email),
                    Description = "Email Exists"
                });
            }

            if (userByName is not null)
            {
                UserErrors.Add(new IdentityError()
                {
                    Code = nameof(NewUserVM.UserName),
                    Description = "UserName Exists"
                });
            }

            return IdentityResult.Failed(UserErrors.ToArray());
        }

        public bool IsSignedIn(ClaimsPrincipal claims)
        {
            return _signInManager.IsSignedIn(claims);
        }

        public async Task SignIn(string UserName, bool IsPersistent = false)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user is not null)
            {
                await _signInManager.SignInAsync(user, IsPersistent);
            }
        }

        public async Task<SignInResult> Login(LogInVM LoggedUser)
        {
            var (userByEmail, userByName) = await GetUser(LoggedUser.UserName, LoggedUser.UserName);
            var user = userByEmail ?? userByName;

            if (user is not null)
            {
                return await _signInManager.PasswordSignInAsync(user,
                    LoggedUser.Password,
                    LoggedUser.RememberMe,
                    lockoutOnFailure: false);
            }
            return SignInResult.Failed;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<(User?, User?)> GetUser(string Email, string userName)
        {
            var userByEmail = await _userManager.FindByEmailAsync(Email);
            var userByName = await _userManager.FindByNameAsync(userName);

            return (userByEmail, userByName);
        }
    }
}
