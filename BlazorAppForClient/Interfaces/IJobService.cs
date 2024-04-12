using BlazorAppForClient.ViewModels;

namespace BlazorAppForClient.Interfaces
{
    public interface IJobService
    {

        Task<IEnumerable<CompleteJobViewModel>> GetAsync();
        Task SubmitAJob(NewJoobViewModel newJoobViewModel);



    }
}
