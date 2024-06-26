using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using FastScooter.API.Request;
using FastScooter.API.Response;
using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.API.Controllers;

[Route("api/v1/scooter")]
[ApiController]
public class ScooterController : ControllerBase
{
    // Dependency Injection
    private readonly IScooterInfrastructure _scooterInfrastructure;
    private readonly IScooterDomain _scooterDomain;
    private readonly IMapper _mapper;
    
    // ScooterController Constructor
    public ScooterController(
        IScooterInfrastructure scooterInfrastructure,
        IScooterDomain scooterDomain,
        IMapper mapper
        )
    {
        _scooterInfrastructure = scooterInfrastructure;
        _scooterDomain = scooterDomain;
        _mapper = mapper;
    }
    
    // Endpoints with HTTP Methods (GET, POST, PUT, DELETE)
    
    // GET: api/v1/scooter
    [HttpGet(Name = "GetScooter")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var scooters = await _scooterInfrastructure.GetScootersAsync();
            var result = _mapper.Map<List<ScooterResponse>>(scooters);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    // GET: api/v1/scooter/{id}
    [HttpGet("{id:int}", Name = "GetScooterById")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var scooter = await _scooterInfrastructure.GetScooterByIdAsync(id);
            var result = _mapper.Map<Scooter, ScooterResponse>(scooter);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    // POST: api/v1/scooter
    [HttpPost(Name = "PostScooter")]
    public async Task<IActionResult> Post([FromBody] ScooterRequest input)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var scooter = _mapper.Map<ScooterRequest, Scooter>(input);
            var result = await _scooterDomain.CreateScooterAsync(scooter);
            
            return result > 0 ? StatusCode(StatusCodes.Status201Created, result) : BadRequest();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    // PUT: api/v1/scooter/{id}
    [HttpPut("{id:int}", Name = "PutScooter")]
    public async Task<IActionResult> Put(int id, [FromBody] ScooterRequest input)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var scooter = _mapper.Map<ScooterRequest, Scooter>(input);
            var result = await _scooterDomain.UpdateScooterAsync(id, scooter);
            
            return result ? StatusCode(StatusCodes.Status200OK) : BadRequest();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    // DELETE: api/v1/scooter/{id}
    [HttpDelete("delete/{id:int}", Name = "DeleteScooter")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _scooterDomain.DeleteScooterAsync(id);
            return result ? StatusCode(StatusCodes.Status200OK) : BadRequest();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}
