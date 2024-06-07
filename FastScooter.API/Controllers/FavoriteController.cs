using AutoMapper;
using FastScooter.API.Request;
using FastScooter.API.Response;
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
          return _favoriteDomain.CreateNewFavorite(favorite);
     }
     
     
  
     [HttpGet("userId/{userId}", Name = "GetAllFavoritesByUserId")]
     public async Task<ActionResult<List<FavoriteResponse>>> GetAllByUserId(int userId)
     {
          try
          {
               var favorites = await _favoriteDomain.GetAllByUserId(userId);

               // Mapea la lista de favoritos al tipo de respuesta esperado
               var favoriteResponses = _mapper.Map<List<FavoriteResponse>>(favorites);

               return Ok(favoriteResponses);
          }
          catch (Exception ex)
          {
               return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
          }
     }
     
     [HttpGet("scooterId/{userId}", Name = "GetAllFavoritesScooterId")]
     public async Task<ActionResult<List<FavoriteResponse>>> GetAllByScooterId(int userId)
     {
          try
          {
               var favorites = await _favoriteDomain.GetAllByScooterId(userId);

               var favoriteResponses = _mapper.Map<List<FavoriteResponse>>(favorites);

               return Ok(favoriteResponses);
          }
          catch (Exception ex)
          {
               return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
          }
     }
     // DELETE: api/v1/user/{id}
     [HttpDelete("{id:int}", Name = "DeleteFavorite")]
     public async Task<IActionResult> Delete(int id)
     {
          try
          {
               var result = await _favoriteDomain.CancelFavorite(id);
               return result ? StatusCode(StatusCodes.Status200OK) : BadRequest();
          }
          catch (Exception e)
          {
               return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
          }
     }
     
}