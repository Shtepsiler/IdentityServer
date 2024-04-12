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

/*            CreateJobMap();
            CreateManagerMap();
            CreateModelMap();
            CreateIdentityMap();
            CreateClientMap();
            CreateMechanicMap();*/
        }
        private void CreateUserMaps()
        {
            CreateMap<UserSignUpRequest, User>().ReverseMap();
            CreateMap<UserRequest, User>().ReverseMap();
            CreateMap<UserResponse, User>().ReverseMap();
          


            /*            CreateMap<User, UserResponse>()
                            .ForMember(
                                response => response.FullName,
                                options => options.MapFrom(user => $"{user.FirstName} {user.LastName}"))
                            .ForMember(
                                response => response.Avatar,
                                options => options.MapFrom(
                                    user => !string.IsNullOrWhiteSpace(user.Avatar) ? $"Public/Photos/{user.Avatar}" : null));*/
        }


        /* private void CreateJobMap()
         {
             CreateMap<Job, ClientsJobsResponse>()
                 .ForMember(r => r.ManagerName, opt => opt.MapFrom(manager => $"{manager.Manager.FullName}"))
                 .ForMember(r => r.ManagerPhone, opt => opt.MapFrom(manager => $"{manager.Manager.PhoneNumber}"))
                 .ForMember(r => r.MechanicFullName, opt => opt.MapFrom(mech => $"{mech.Mechanic.FirstName} {mech.Mechanic.LastName}"))
                 .ForMember(r => r.Model, opt => opt.MapFrom(model => model.Model.Name))
                 ;
             CreateMap<Job, JobResponse>().ReverseMap();
             CreateMap<JobRequest, Job>().ReverseMap();
         }

         private void CreateManagerMap()
         {
             CreateMap<Manager, ManagerResponse>();
             CreateMap<ManagerRequest, Manager>();


         }
         private void CreateMechanicMap()
         {
             CreateMap<Mechanic, MechanicResponse>().ReverseMap();
             CreateMap<Mechanic, MechanicPublicResponse>().ReverseMap();

         }
         private void CreateModelMap()
         {
             CreateMap<Model, ModelResponse>();
             CreateMap<ModelRequest, Model>();
         }
         private void CreateIdentityMap()
         {
             CreateMap<Client, ClientSignUpRequest>().ReverseMap();
         }
         private void CreateClientMap()
         {
             CreateMap<Client, ClientResponse>().ForMember(r => r.ClientName, opt => opt.MapFrom(client => client.UserName)).ReverseMap();
             CreateMap<ClientRequest, Client>().ForMember(r => r.UserName, opt => opt.MapFrom(client => client.ClientName)).ReverseMap();

         }*/




        /*
                private void CreateClientMaps()
                {
                    CreateMap<UserSignUpRequest, User>();
                    CreateMap<UserRequest, User>();
                    CreateMap<User, UserResponse>()
                        .ForMember(
                            response => response.FullName,
                            options => options.MapFrom(user => $"{user.FirstName} {user.LastName}"))
                        .ForMember(
                            response => response.Avatar,
                            options => options.MapFrom(
                                user => !string.IsNullOrWhiteSpace(user.Avatar) ? $"Public/Photos/{user.Avatar}" : null));
                }

                private void CreateTicketsMaps()
                {
                    CreateMap<TicketRequest, Ticket>();
                    CreateMap<Ticket, TicketResponse>();
                }

                private void CreateTeamsMaps()
                {
                    CreateMap<TeamRequest, Team>();
                    CreateMap<Team, TeamResponse>()
                        .ForMember(
                            response => response.Avatar,
                            options => options.MapFrom(
                                team => !string.IsNullOrWhiteSpace(team.Avatar) ? $"Public/Photos/{team.Avatar}" : null));
                }

                private void CreateProjectsMaps()
                {
                    CreateMap<ProjectRequest, Project>();
                    CreateMap<Project, ProjectResponse>();
                }

                private void CreateRatingsMaps()
                {
                    CreateMap<RatingRequest, Rating>();
                    CreateMap<Rating, RatingResponse>().ForMember(
                        response => response.Average,
                        options => options.MapFrom(
                            rating => (rating.Skills
                                       + rating.Social
                                       + rating.Punctuality
                                       + rating.Responsibility) / 4));
                }
        */

    }
}
