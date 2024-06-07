using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using FastScooter.API.Request;
using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.API.Controllers;

[Route("api/v1/rent")]
[ApiController]
public class RentController : ControllerBase
{
    // Dependency Injection
    private readonly IRentInfrastructure _rentInfrastructure;
    private readonly IRentDomain _rentDomain;
    private readonly IMapper _mapper;
    
    // RentController Constructor
    public RentController(
        IRentInfrastructure rentInfrastructure,
        IRentDomain rentDomain,
        IMapper mapper
        )
    {
        _rentInfrastructure = rentInfrastructure;
        _rentDomain = rentDomain;
        _mapper = mapper;
    }
    
    // Endpoints with HTTP Methods (GET && POST)
    
    // ToDO: Get: api/v1/rent/available
    // [HttpGet("available/{bikeId}", Name = "GetAvailableScooters")]
    // public async Task<IActionResult> GetAvailableScooters()
    // {
    //     
    // }
    
    // POST: api/v1/rent
    [HttpPost(Name = "PostRent")]
    public async Task<IActionResult> Post([FromBody] RentRequest input)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var rent = _mapper.Map<RentRequest, Rent>(input);
            var result = await _rentDomain.CreateRentAsync(rent);
            
            return result > 0 ? StatusCode(StatusCodes.Status201Created, result) : BadRequest();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    // GET: api/rent/available
    [HttpGet("available/{scooterId}", Name = "GetAvailableScooters")]
    public bool GetAvailableScooters(int bikeId, DateTime start, DateTime end)
    {
        return _rentDomain.AvailableScooter(bikeId, start, end);
    }
    
    
    // GET: api/rent/available
    [HttpGet("available/", Name = "GetAllAvailableScootersForDate")]
    public List<Scooter> GetAvailableScooters( DateTime start, DateTime end)
    {
        return _rentDomain.GetAvailableScooters( start, end);
    }
    
    
    // DELETE: api/rent/5
    [HttpDelete("{id}", Name = "DeleteRent")]
    public void Delete(int id)
    {
        _rentDomain.CancelUnfinishedRent(id);
    }
}
