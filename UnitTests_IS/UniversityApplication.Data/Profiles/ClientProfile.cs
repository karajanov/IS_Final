using AutoMapper;
using BankApplication.Data.DTOs;
using BankApplication.Data.Models;

namespace BankApplication.Data.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDTO>()
                .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.Email))
                .ReverseMap()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Mail));
        }
    }
}