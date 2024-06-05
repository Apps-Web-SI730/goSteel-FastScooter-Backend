using AutoMapper;
using FastScooter.API.Request;
using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace FastScooter.API.Controllers;



[Route("api/v1/favorite")]
[ApiController]
public class FavoriteController: ControllerBase
{
     private IFavoriteDomain _favoriteDomain;
     private IMapper _mapper;

     public FavoriteController(IFavoriteDomain favoriteDomain, IMapper mapper)
     {
          _favoriteDomain = favoriteDomain;
          _mapper = mapper;
     }

     [HttpPost(Name = "PostFavorites")]
     public bool Post([FromBody] FavoriteRequest favoriteRequest)
     {
          var favorite = _mapper.Map<FavoriteRequest, Favorites>(favoriteRequest);
          return _favoriteDomain.Save(favorite);
     }

     
     
}