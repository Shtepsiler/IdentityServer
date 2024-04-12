using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorAppForClient.ViewModels;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorAppForClient.Interfaces
{
    public interface IUsersService
    {


        Task<UserViewModel> GetByNameAsync();

  

        Task UpdateAsync( UserViewModel viewModel);


        Task DeleteAsync();


    }
}
