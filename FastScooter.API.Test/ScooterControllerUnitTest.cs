using Xunit;
using Moq;
using AutoMapper;
using FastScooter.API.Controllers;
using FastScooter.API.Request;
using FastScooter.API.Response;
using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FastScooter.API.Test;

public class ScooterControllerUnitTest
{
    [Fact]
    public async Task Post_WhenModelStateIsInvalid_ReturnsBadRequest()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockScooterDomain = new Mock<IScooterDomain>();
        var scooterRequest = new Mock<ScooterRequest>();
        var controller =
            new ScooterController(mockScooterInfrastructure.Object, mockScooterDomain.Object, mockMapper.Object);
        controller.ModelState.AddModelError("Error", "Model state is invalid");

        // Act
        var result = await controller.Post(scooterRequest.Object);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Post_WhenCreateScooterAsyncReturnsZero_ReturnsBadRequest()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockScooterDomain = new Mock<IScooterDomain>();
        var scooterRequest = new Mock<ScooterRequest>();
        var scooter = new Scooter
        {
            Name = "Test Scooter",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            ImageUrl = "TestImageUrl.jpg"
        };
        mockMapper.Setup(x => x.Map<Scooter>(scooterRequest)).Returns(scooter);
        mockScooterInfrastructure.Setup(x => x.CreateScooterAsync(It.IsAny<Scooter>())).ReturnsAsync(0);
        var controller =
            new ScooterController(mockScooterInfrastructure.Object, mockScooterDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Post(scooterRequest.Object);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Get_WhenExceptionIsThrown_ReturnsStatusCode500InternalServerError()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockScooterDomain = new Mock<IScooterDomain>();
        mockScooterInfrastructure.Setup(x => x.GetScootersAsync()).ThrowsAsync(new Exception());
        var controller =
            new ScooterController(mockScooterInfrastructure.Object, mockScooterDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Get();

        // Assert
        var statusCode500InternalServerError = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCode500InternalServerError.StatusCode);
    }

    [Fact]
    public async Task Get_ById_WhenExceptionIsThrown_ReturnsStatusCode500InternalServerError()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockScooterDomain = new Mock<IScooterDomain>();
        mockScooterInfrastructure.Setup(x => x.GetScooterByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception());
        var controller =
            new ScooterController(mockScooterInfrastructure.Object, mockScooterDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Get(1);

        // Assert
        var statusCode500InternalServerError = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusCode500InternalServerError.StatusCode);
    }
    
    [Fact]
    public async Task Get_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockScooterDomain = new Mock<IScooterDomain>();
        var mockMapper = new Mock<IMapper>();
        var scooters = new List<Scooter>();
        var scooterResponses = new List<ScooterResponse>();
        mockScooterInfrastructure.Setup(x => x.GetScootersAsync()).ReturnsAsync(scooters);
        mockMapper.Setup(x => x.Map<List<ScooterResponse>>(It.IsAny<List<Scooter>>())).Returns(scooterResponses);
        var controller = new ScooterController(mockScooterInfrastructure.Object, mockScooterDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ScooterResponse>>(okResult.Value);
        Assert.Equal(scooterResponses, returnValue);
    }

    [Fact]
    public async Task Delete_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockScooterDomain = new Mock<IScooterDomain>();
        var mockMapper = new Mock<IMapper>();
        mockScooterDomain.Setup(x => x.DeleteScooterAsync(It.IsAny<int>())).ReturnsAsync(true);
        var controller = new ScooterController(mockScooterInfrastructure.Object, mockScooterDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Delete(1);

        // Assert
        var okResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
    }
    
    [Fact]
    public async Task Put_WhenModelStateIsInvalid_ReturnsBadRequest()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockScooterDomain = new Mock<IScooterDomain>();
        var scooterRequest = new Mock<ScooterRequest>();
        var controller = new ScooterController(mockScooterInfrastructure.Object, mockScooterDomain.Object, mockMapper.Object);
        controller.ModelState.AddModelError("Error", "Model state is invalid");

        // Act
        var result = await controller.Put(1, scooterRequest.Object);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task Put_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockScooterDomain = new Mock<IScooterDomain>();
        var scooterRequest = new ScooterRequest
        {
            Name = "Test Scooter",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            ImageUrl = "TestImageUrl.jpg"
        };
        var scooter = new Scooter
        {
            Name = "Test Scooter",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            ImageUrl = "TestImageUrl.jpg"
        };
        mockMapper.Setup(x => x.Map<ScooterRequest, Scooter>(It.IsAny<ScooterRequest>())).Returns(scooter);
        mockScooterDomain.Setup(x => x.UpdateScooterAsync(It.IsAny<int>(), It.IsAny<Scooter>())).ReturnsAsync(true);
        var controller = new ScooterController(mockScooterInfrastructure.Object, mockScooterDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Put(1, scooterRequest);

        // Assert
        var okResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
    }

    [Fact]
    public async Task Get_ById_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockScooterDomain = new Mock<IScooterDomain>();
        var scooter = new Scooter
        {
            Name = "Test Scooter",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            ImageUrl = "TestImageUrl.jpg"
        };
        var scooterResponse = new ScooterResponse
        {
            Name = "Test Scooter",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            ImageUrl = "TestImageUrl.jpg"
        };
        mockScooterInfrastructure.Setup(x => x.GetScooterByIdAsync(It.IsAny<int>())).ReturnsAsync(scooter);
        mockMapper.Setup(x => x.Map<Scooter, ScooterResponse>(It.IsAny<Scooter>())).Returns(scooterResponse);
        var controller = new ScooterController(mockScooterInfrastructure.Object, mockScooterDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Get(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ScooterResponse>(okResult.Value);
        Assert.Equal(scooterResponse, returnValue);
    }    
    
    [Fact]
    public async Task Get_WhenCalledWithId_ReturnsOkResult()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockScooterDomain = new Mock<IScooterDomain>();
        var scooter = new Scooter
        {
            Name = "Test Scooter",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            ImageUrl = "TestImageUrl.jpg"
        };
        var scooterResponse = new ScooterResponse
        {
            Name = "Test Scooter",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            ImageUrl = "TestImageUrl.jpg"
        };
        mockScooterInfrastructure.Setup(x => x.GetScooterByIdAsync(It.IsAny<int>())).ReturnsAsync(scooter);
        var controller = new ScooterController(mockScooterInfrastructure.Object, mockScooterDomain.Object, mockMapper.Object);

            
        // Act
        var result = await controller.Get(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
    
    
   /*[Fact]
    public async Task Post_WhenCalled_ReturnsCreatedResult()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockScooterDomain = new Mock<IScooterDomain>();
        var scooterRequest = new ScooterRequest
        {
            Name = "Test Scooter",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            ImageUrl = "TestImageUrl.jpg"
        };
        var scooter = new Scooter
        {
            Name = "Test Scooter",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            ImageUrl = "TestImageUrl.jpg"
        };
        mockMapper.Setup(m => m.Map<ScooterRequest, Scooter>(It.IsAny<ScooterRequest>())).Returns(scooter);
        mockScooterDomain.Setup(x => x.CreateScooterAsync(It.IsAny<Scooter>())).ReturnsAsync(1);
        var controller = new ScooterController(mockScooterInfrastructure.Object, mockScooterDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Post(scooterRequest);

        // Assert
        Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(201, ((StatusCodeResult)result).StatusCode);
    }*/
    
    
    [Fact]
    public async Task Post_WhenCalledWithNull_ReturnsBadRequest()
    {
        // Arrange
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockScooterDomain = new Mock<IScooterDomain>();
        var mockMapper = new Mock<IMapper>();
        var controller = new ScooterController(mockScooterInfrastructure.Object, mockScooterDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Post(null);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

}