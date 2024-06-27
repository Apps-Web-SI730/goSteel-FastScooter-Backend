using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using FastScooter.API.Request;
using FastScooter.API.Response;
using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Dtos;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;

namespace FastScooter.API.Controllers;

[Route("api/v1/user")]
[ApiController]
public class UserController : ControllerBase
{
    // Dependency Injection
    private readonly IUserInfrastructure _userInfrastructure;
    private readonly IUserDomain _userDomain;
    private readonly IMapper _mapper;
    
    // UserController Constructor
    public UserController(
        IUserInfrastructure userInfrastructure,
        IUserDomain userDomain,
        IMapper mapper
        )
    {
        _userInfrastructure = userInfrastructure;
        _userDomain = userDomain;
        _mapper = mapper;
    }
    
    // Endpoints with HTTP Methods (GET, POST, PUT, DELETE)
    
    // GET: api/v1/user
    [HttpGet(Name = "GetUser")]
    public async Task<IActionResult> Get()
    {
        try
        {
            var users = await _userInfrastructure.GetUsersAsync();
            var result = _mapper.Map<List<User>, List<UserResponse>>(users);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    // GET: api/v1/user/{id}
    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var user = await _userInfrastructure.GetUserByIdAsync(id);
            var result = _mapper.Map<User, UserResponse>(user);
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    // POST: api/v1/user
    [HttpPost(Name = "PostUser")]
    public async Task<IActionResult> Post([FromBody] UserRequest input)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var user = _mapper.Map<UserRequest, User>(input);
            var result = await _userDomain.CreateUserAsync(user);

            return result > 0 ? StatusCode(StatusCodes.Status201Created, result) : BadRequest();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    // PUT: api/v1/user/{id}
    [HttpPut("{id:int}", Name = "PutUser")]
    public async Task<IActionResult> Put(int id, UserDto value)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _userDomain.UpdateUserAsync(id, value);
            return result ? StatusCode(StatusCodes.Status200OK) : BadRequest();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    // DELETE: api/v1/user/{id}
    [HttpDelete("{id:int}", Name = "DeleteUser")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _userDomain.DeleteUserAsync(id);
            return result ? StatusCode(StatusCodes.Status200OK) : BadRequest();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
    // POST: api/user/loginrev1
    [AllowAnonymous]
    [HttpPost]
    [Route("loginrev1")]
    public async Task<IActionResult> LoginRev1([FromBody] LoginRequest userInput)
    {
        try
        {
            var user = _mapper.Map<LoginRequest, User>(userInput);
            var response = await _userDomain.LoginRev1(user);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw;
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar");
        }
    }
    //ESTOY UTILIZANDO ESTA PETICION
    // POST: api/user/signuprev1
    [AllowAnonymous]
    [HttpPost]
    [Route("signuprev1")]
    public async Task<IActionResult> SignupRev1([FromBody] UserRequest userInput)
    {
        try
        {
            var user = _mapper.Map<UserRequest, User>(userInput);
            var response = await _userDomain.SignupRev1(user);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw;
            return StatusCode(StatusCodes.Status500InternalServerError, "Error al procesar");
        }
    }
}