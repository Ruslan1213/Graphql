using AutoMapper;
using GraphQlLibary.Domain.Models;
using GraphQlLibary.Models.ViewModels;

namespace GraphQlLibary.Infrastructure.Mapping
{
    public class DomainProfile : AutoMapper.Profile
    {
        public DomainProfile()
        {
            CreateMap<User, RegisterViewModel>().ReverseMap();
        }
    }
}
