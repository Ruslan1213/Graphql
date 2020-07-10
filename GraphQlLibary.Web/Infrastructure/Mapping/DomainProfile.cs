using AutoMapper;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Models.ViewModels;

namespace GraphQlLibary.Web.Infrastructure.Mapping
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<User, RegisterViewModel>().ReverseMap();
        }
    }
}
