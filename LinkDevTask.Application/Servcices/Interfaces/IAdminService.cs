using LinkDevTask.Application.ViewModels.Role;
using LinkDevTask.Application.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDevTask.Application.Servcices.Interfaces
{
    public interface IAdminService
    {
        Task<IdentityResult> AddRole(string newRole);
        Task<IdentityResult> DeleteRole(string role);
        Task<IEnumerable<RoleVM>> GetAllRoles();
        Task<IEnumerable<UserVM>> GetAllUsers();
        Task<UserVM?> GetUser(string id);
        Task<List<RoleVM>> GetUserRoles(UserVM User);
        Task<IEnumerable<IdentityResult>> ManageUserRoles(UserVM User, List<RoleVM> Roles);
    }
}
