using OnlineStore.BusinessLogic.Implementation.Account.Models;
using System;
using AutoMapper;
using OnlineStore.Entities.Entities;

namespace OnlineStore.BusinessLogic.Implementation.Account.Mapping
{
    public class UserProfile : Profile
    {

        public UserProfile() {
            CreateMap<RegisterModel, User>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName));


        }
    }
}
