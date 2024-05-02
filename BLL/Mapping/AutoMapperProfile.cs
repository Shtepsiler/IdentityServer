using AutoMapper;
using BLL.DTO.Requests;
using BLL.DTO.Responses;
using DAL.Entities;

namespace BLL.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateUserMaps();

        }
        private void CreateUserMaps()
        {
            CreateMap<UserSignUpRequest, User>().ReverseMap();
            CreateMap<UserRequest, User>().ReverseMap();
            CreateMap<UserResponse, User>().ReverseMap();

        }


      

    }
}
