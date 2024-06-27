 using System.Net;
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

 namespace FastScooter.API.Test;

 public class RentControllerUnitTest
 { 
     [Fact]
    public void GetAvailableScooters_ReturnsExpectedResult()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockRentDomain = new Mock<IRentDomain>();
        mockRentDomain.Setup(x => x.AvailableScooter(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);
        var controller = new RentController(mockRentInfrastructure.Object, mockRentDomain.Object, mockMapper.Object);

        // Act
        var result = controller.GetAvailableScooters(1, DateTime.Now, DateTime.Now.AddDays(1));

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetAllAvailableScootersForDate_ReturnsExpectedResult()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockRentDomain = new Mock<IRentDomain>();
        var scooters = new List<Scooter>();
        mockRentDomain.Setup(x => x.GetAvailableScooters(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(scooters);
        var controller = new RentController(mockRentInfrastructure.Object, mockRentDomain.Object, mockMapper.Object);

        // Act
        var result = controller.GetAvailableScooters(DateTime.Now, DateTime.Now.AddDays(1));

        // Assert
        Assert.IsAssignableFrom<List<Scooter>>(result);
    }

    [Fact]
    public void Delete_ReturnsNoContentResult()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockRentDomain = new Mock<IRentDomain>();
        mockRentDomain.Setup(x => x.CancelUnfinishedRent(It.IsAny<int>()));
        var controller = new RentController(mockRentInfrastructure.Object, mockRentDomain.Object, mockMapper.Object);

        // Act
        controller.Delete(1);

        // Assert
        mockRentDomain.Verify(x => x.CancelUnfinishedRent(It.IsAny<int>()), Times.Once);
    }
    
    [Fact]
    public async Task Post_WhenModelStateIsInvalid_ReturnsBadRequest()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockRentDomain = new Mock<IRentDomain>();
        var rentRequest = new RentRequest();
        var controller = new RentController(mockRentInfrastructure.Object, mockRentDomain.Object, mockMapper.Object);
        controller.ModelState.AddModelError("Error", "Model state is invalid");

        // Act
        var result = await controller.Post(rentRequest);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task Get_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockRentDomain = new Mock<IRentDomain>();
        var mockMapper = new Mock<IMapper>();
        var rents = new List<Rent>();
        var rentResponses = new List<RentResponse>();
        mockRentInfrastructure.Setup(x => x.GetAll()).ReturnsAsync(rents);
        mockMapper.Setup(x => x.Map<List<RentResponse>>(It.IsAny<List<Rent>>())).Returns(rentResponses);
        var controller = new RentController(mockRentInfrastructure.Object, mockRentDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Get();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<RentResponse>>(okResult.Value);
        Assert.Equal(rentResponses, returnValue);
    }

    [Fact]
    public void GetAvailableScooters_WhenCalled_ReturnsTrue()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockRentDomain = new Mock<IRentDomain>();
        var mockMapper = new Mock<IMapper>();
        mockRentDomain.Setup(x => x.AvailableScooter(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(true);
        var controller = new RentController(mockRentInfrastructure.Object, mockRentDomain.Object, mockMapper.Object);

        // Act
        var result = controller.GetAvailableScooters(1, DateTime.Now, DateTime.Now);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetAvailableScootersForDate_WhenCalled_ReturnsList()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockRentDomain = new Mock<IRentDomain>();
        var mockMapper = new Mock<IMapper>();
        var scooters = new List<Scooter>();
        mockRentDomain.Setup(x => x.GetAvailableScooters(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(scooters);
        var controller = new RentController(mockRentInfrastructure.Object, mockRentDomain.Object, mockMapper.Object);

        // Act
        var result = controller.GetAvailableScooters(DateTime.Now, DateTime.Now);

        // Assert
        Assert.Equal(scooters, result);
    }

    [Fact]
    public void Delete_WhenCalled_ReturnsNoContentResult()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockRentDomain = new Mock<IRentDomain>();
        var mockMapper = new Mock<IMapper>();
        var controller = new RentController(mockRentInfrastructure.Object, mockRentDomain.Object, mockMapper.Object);

        // Act
        controller.Delete(1);

        // Assert
        mockRentDomain.Verify(x => x.CancelUnfinishedRent(It.IsAny<int>()), Times.Once);
    }
    
    [Fact]
    public async Task Post_WhenCalledWithNull_ReturnsBadRequest()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockRentDomain = new Mock<IRentDomain>();
        var mockMapper = new Mock<IMapper>();
        var controller = new RentController(mockRentInfrastructure.Object, mockRentDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Post(null);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }
    
    [Fact]
    public async Task Post_WhenCalledWithInvalidRent_ReturnsBadRequest()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockRentDomain = new Mock<IRentDomain>();
        var rentRequest = new RentRequest();
        var rent = new Rent();
        mockMapper.Setup(x => x.Map<RentRequest, Rent>(rentRequest)).Returns(rent);
        mockRentDomain.Setup(x => x.CreateRentAsync(rent)).Throws(new Exception());
        var controller = new RentController(mockRentInfrastructure.Object, mockRentDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Post(rentRequest);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestResult>(result);
    }
    
 }