using AutoMapper;

using FastScooter.API.Response;
using FastScooter.Infrastructure.Models;

namespace FastScooter.API.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<User, UserResponse>();
        CreateMap<Favorites, FavoriteResponse>();
        CreateMap<Payment, PaymentResponse>();


    }
}