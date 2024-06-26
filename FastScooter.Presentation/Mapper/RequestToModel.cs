using AutoMapper;

using FastScooter.API.Request;
using FastScooter.Infrastructure.Models;

namespace FastScooter.API.Mapper;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<UserRequest, User>();
        CreateMap<ScooterRequest, Scooter>().ForMember(dest => dest.Price, opt => opt.MapFrom(src => (decimal)src.Price));
        ;
        CreateMap<RentRequest, Rent>();
        CreateMap<FavoriteRequest, Favorites>();
        CreateMap<PaymentRequest, Payment>();
    }
}
