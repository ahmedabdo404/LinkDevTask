namespace LinkDevTask.Application.ViewModels.Role
{
    public class RoleVM
    {
        public string RoleName { get; set; } = null!;
        public int UsersInRoleCount { get; set; }
        public bool IsSelected { get; set; }
    }
}
