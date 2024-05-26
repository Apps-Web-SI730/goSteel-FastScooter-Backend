using AutoMapper;

using FastScooter.API.Request;
using FastScooter.Infrastructure.Models;

namespace FastScooter.API.Mapper;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<UserRequest, User>();
        CreateMap<ScooterRequest, Scooter>();
        CreateMap<RentRequest, Rent>();
    }
}