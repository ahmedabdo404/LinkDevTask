using LinkDevTask.Domain.Models;
using LinkDevTask.Application.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevTask.Application.Servcices.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> CreateUser(RegisterVM NewUserVM);
        Task<(User?, User?)> GetUser(string Email, string userName);
        bool IsSignedIn(ClaimsPrincipal claims);
        Task<SignInResult> Login(LogInVM LoggedUser);
        Task Logout();
        Task SignIn(string UserName, bool IsPersistent = false);
    }
}
