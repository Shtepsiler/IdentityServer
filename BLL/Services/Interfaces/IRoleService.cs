using DAL.Entities;

namespace BLL.Services.Interfaces
{
    public interface IRoleService
    {
        Task AssignRole(Guid id, string role);
        Task UnAssignRole(Guid id, string role);

        Task<IEnumerable<Role>> GetRolesAsync();
        Task DeleteRole(string name);
        Task UpdateRole(string name);
        Task CreateRole(string name);
        Task<IEnumerable<string>> GetRolesForUserAsync(Guid id);
    }
}