using Xunit;
using Moq;
using AutoMapper;
using FastScooter.API.Controllers;
using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FastScooter.API.Request;
using FastScooter.API.Response;
using FastScooter.Infrastructure.Dtos;

namespace FastScooter.API.Test;

public class UserControllerUnitTest
{
    [Fact]
    public async Task Get_WhenExceptionIsThrown_ReturnsStatusCode500InternalServerError()
    {
        // Arrange
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockUserDomain = new Mock<IUserDomain>();
        var mockMapper = new Mock<IMapper>();
        mockUserInfrastructure.Setup(x => x.GetUsersAsync()).ThrowsAsync(new Exception());
        var controller = new UserController(mockUserInfrastructure.Object, mockUserDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Get();

        // Assert
        var statusCodeResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task Get_ById_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockUserDomain = new Mock<IUserDomain>();
        var mockMapper = new Mock<IMapper>();
        var user = new User
        {
            Name = "Test User",
            Email = "testuser@example.com",
            Password = "password123",
            Role = "Admin"
        };
        var userResponse = new UserResponse
        {
            Name = "Test User",
            Email = "testuser@example.com",
        };
        mockUserInfrastructure.Setup(x => x.GetUserByIdAsync(It.IsAny<int>())).ReturnsAsync(user);
        mockMapper.Setup(m => m.Map<User, UserResponse>(It.IsAny<User>())).Returns(userResponse);
        var controller = new UserController(mockUserInfrastructure.Object, mockUserDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Get(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<UserResponse>(okResult.Value);
        Assert.Equal(user.Name, returnValue.Name);
    }

    [Fact]
    public async Task Put_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockUserDomain = new Mock<IUserDomain>();
        var mockMapper = new Mock<IMapper>();
        var userDto = new UserDto
        {
            Name = "Test User",
            Email = "testuser@example.com",
            Password = "password123"
        };
        mockUserDomain.Setup(x => x.UpdateUserAsync(It.IsAny<int>(), It.IsAny<UserDto>())).ReturnsAsync(true);
        var controller = new UserController(mockUserInfrastructure.Object, mockUserDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Put(1, userDto);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(200, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task Delete_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockUserDomain = new Mock<IUserDomain>();
        var mockMapper = new Mock<IMapper>();
        mockUserDomain.Setup(x => x.DeleteUserAsync(It.IsAny<int>())).ReturnsAsync(true);
        var controller = new UserController(mockUserInfrastructure.Object, mockUserDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Delete(1);

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(200, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task Get_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockUserDomain = new Mock<IUserDomain>();
        var mockMapper = new Mock<IMapper>();
        var users = new List<User>
        {
            new User
            {
                Name = "Test User",
                Email = "testuser@example.com",
                Password = "password123",
                Role = "Admin"
            }
        };
        var userResponses = new List<UserResponse>
        {
            new UserResponse
            {
                Name = "Test User",
                Email = "testuser@example.com"
            }
        };
        mockUserInfrastructure.Setup(x => x.GetUsersAsync()).ReturnsAsync(users);
        mockMapper.Setup(m => m.Map<List<User>, List<UserResponse>>(It.IsAny<List<User>>())).Returns(userResponses);
        var controller = new UserController(mockUserInfrastructure.Object, mockUserDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<UserResponse>>(okResult.Value);
        Assert.Equal(userResponses, returnValue);
    }

    [Fact]
    public async Task Post_WhenCalled_ReturnsCreatedResult()
    {
        // Arrange
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockUserDomain = new Mock<IUserDomain>();
        var mockMapper = new Mock<IMapper>();
        var userRequest = new UserRequest
        {
            Name = "Test User",
            Email = "testuser@example.com",
            Password = "password123",
            Role = "Admin"
        };
        var user = new User
        {
            Name = "Test User",
            Email = "testuser@example.com",
            Password = "password123",
            Role = "Admin"
        };
        mockMapper.Setup(m => m.Map<UserRequest, User>(It.IsAny<UserRequest>())).Returns(user);
        mockUserDomain.Setup(x => x.CreateUserAsync(It.IsAny<User>())).ReturnsAsync(1);
        var controller = new UserController(mockUserInfrastructure.Object, mockUserDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Post(userRequest);

        // Assert
        var objectResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(201, objectResult.StatusCode);
    }

    [Fact]
    public async Task Post_WhenCalledWithNull_ReturnsBadRequest()
    {
        // Arrange
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockUserDomain = new Mock<IUserDomain>();
        var mockMapper = new Mock<IMapper>();
        var controller = new UserController(mockUserInfrastructure.Object, mockUserDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Post(null);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }


    [Fact]
    public async Task Put_WhenCalledWithNull_ReturnsBadRequest()
    {
        // Arrange
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockUserDomain = new Mock<IUserDomain>();
        var mockMapper = new Mock<IMapper>();
        var controller = new UserController(mockUserInfrastructure.Object, mockUserDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Put(1, null);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
}
