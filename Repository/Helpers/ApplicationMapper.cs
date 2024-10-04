using AutoMapper;
using BusinessObject.Models;
using DataAccessObject.ViewModels;
using BC = global::BCrypt.Net.BCrypt;

namespace Repository.Helpers;
public class ApplicationMapper : Profile
{

    public ApplicationMapper()
    {
        // CRUD MAPPING

        // ACCESS MAPPING
        CreateMap<Account, AccountInfo>()
            .ReverseMap();

        CreateMap<SignupModel, Account>()
            .ForMember(dest => dest.AccountId, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BC.EnhancedHashPassword(src.Password, 13)))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue ? DateOnly.FromDateTime(src.DateOfBirth.Value) : (DateOnly?)null))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));

        CreateMap<AccountInfo, Account>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));
    }
}
