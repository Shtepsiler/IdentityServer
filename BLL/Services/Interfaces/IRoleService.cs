namespace BLL.Services.Interfaces
{
    public interface IRoleService
    {
        Task AssignRole(Guid id, string role);
        Task UnAssignRole(Guid id, string role);
    }
}