namespace BLL.Services.Interfaces
{
    public interface IRoleService
    {
        Task AsignRole(Guid id, string role);
    }
}