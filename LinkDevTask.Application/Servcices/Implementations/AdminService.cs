using LinkDevTask.Application.Helpers;
using LinkDevTask.Domain.Models;
using LinkDevTask.Application.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using LinkDevTask.Application.Servcices.Interfaces;
using LinkDevTask.Application.ViewModels.Role;

namespace LinkDevTask.Application.Servcices.Implementations
{
    public class AdminService : IAdminService
    {
        #region ctor

        readonly RoleManager<IdentityRole> _roleManger;
        readonly UserManager<User> _userManager;

        public AdminService(
            RoleManager<IdentityRole> roleManger,
            UserManager<User> userManager)
        {
            _userManager = userManager;
            _roleManger = roleManger;
        }
        #endregion

        #region Roles

        public async Task<IdentityResult> AddRole(string newRole)
        {
            if (!await _roleManger.RoleExistsAsync(newRole))
            {
            return await _roleManger.CreateAsync(new IdentityRole(newRole.Trim()));
            }

            var roleIsExist = new IdentityError();
            roleIsExist.Description = "Role is Already Exist";
            return IdentityResult.Failed(roleIsExist);
        }

        public async Task<IdentityResult> DeleteRole(string role)
        {
            var cuurentRole = await _roleManger.FindByNameAsync(role);
            if (cuurentRole is not null)
            {
                return await _roleManger.DeleteAsync(cuurentRole);
            }

            var RoleNotExist = new IdentityError();
            RoleNotExist.Description = "RoleNotfound";
            return IdentityResult.Failed(RoleNotExist);
        }

        public async Task<IEnumerable<RoleVM>> GetAllRoles()
        {
            var AllRoles = await _roleManger.Roles.ToListAsync();
            return AllRoles.Select(role => new RoleVM
            {
                RoleName = role.Name,
                UsersInRoleCount = _userManager.GetUsersInRoleAsync(role.Name).Result.Count
            });
        }

        public async Task<List<RoleVM>> GetUserRoles(UserVM User)
        {
            var Model = Mapper.MapTo<User>(User);
            var UserRoles = await _userManager.GetRolesAsync(Model);
            return _roleManger.Roles.Select(role => new RoleVM
            {
                RoleName = role.Name,
                IsSelected = UserRoles.Contains(role.Name),
            }).ToList();
        }

        public async Task<IEnumerable<IdentityResult>> ManageUserRoles(UserVM User, List<RoleVM> Roles)
        {
            var UserModel = await _userManager.FindByNameAsync(User.UserName);
            var rolesResult = new List<IdentityResult>();
            var UserRoles = await _userManager.GetRolesAsync(UserModel);
            foreach (var role in Roles)
            {
                if (UserRoles.Any(r => r == role.RoleName) && !role.IsSelected)
                {
                    rolesResult.Add(await _userManager.RemoveFromRoleAsync(UserModel, role.RoleName));
                }
                else if (!UserRoles.Any(r => r == role.RoleName) && role.IsSelected)
                {
                    rolesResult.Add(await _userManager.AddToRoleAsync(UserModel, role.RoleName));
                }
            }

            return rolesResult.AsEnumerable();
        }
        #endregion

        #region Users

        public async Task<IEnumerable<UserVM>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.Select(user => Mapper.MapTo<UserVM>(user));
        }

        public async Task<UserVM?> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is not null)
            {
                return Mapper.MapTo<UserVM>(user);
            }
            return default;
        }

        #endregion
    }
}
