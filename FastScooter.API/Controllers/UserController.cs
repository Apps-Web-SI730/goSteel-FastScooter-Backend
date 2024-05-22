using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using FastScooter.API.Request;
using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Models;

namespace FastScooter.API.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    // Dependency Injection
    private readonly IUserDomain _userDomain;
    private readonly IMapper _mapper;
    
    // Constructor
    public UserController(IUserDomain userDomain, IMapper mapper)
    {
        _userDomain = userDomain;
        _mapper = mapper;
    }
    
    // ToDo: Return 201 Created
    // ToDo: IActionResult
    // HTTP Methods
    // POST: api/user
    [HttpPost(Name = "PostUser")]
    public void Post([FromBody] UserRequest value)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<UserRequest, User>(value);
            _userDomain.CreateUser(user);
        }
        else
        {
            StatusCode(400);
            throw new Exception("Data was invalid");
        }
    }
}