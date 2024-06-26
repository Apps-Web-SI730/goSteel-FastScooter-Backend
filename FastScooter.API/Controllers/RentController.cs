using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using FastScooter.API.Request;
using FastScooter.API.Response;
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
    
    [HttpGet(Name = "getAllRent")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var rents = await  _rentInfrastructure.GetAll();
            var result = _mapper.Map<List<RentResponse>>(rents);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
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
    
    [HttpGet("userId/{userId}", Name = "GetAllRentByUserId")]
    public async Task<ActionResult<List<RentResponse>>> GetAllByUserId(int userId)
    {
        try
        {
            var payments = await _rentDomain.GetAllByUserId(userId);

            // Mapea la lista de favoritos al tipo de respuesta esperado
            var paymentResponses = _mapper.Map<List<PaymentResponse>>(payments);

            return Ok(paymentResponses);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
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
