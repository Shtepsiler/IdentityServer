using BlazorAppForClient.ViewModels;

namespace BlazorAppForClient.Interfaces
{
    public interface ITeamService
    {
       
        Task<IEnumerable<MechanicViewModel>> GetAllAsync();

    }
}
