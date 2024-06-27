using Xunit;
using Moq;
using FastScooter.Domain.Domain;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastScooter.Domain.Test;
public class RentDomainUnitTest
{
    [Fact]
    public async Task CreateRentAsync_WhenCalledWithValidRent_ReturnsRentId()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var rent = new Rent
        {
            UserId = 1,
            ScooterId = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)
        };
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockScooterInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockRentInfrastructure.Setup(x => x.CreateRentAsync(It.IsAny<Rent>())).ReturnsAsync(1);
        var domain = new RentDomain(mockRentInfrastructure.Object, mockScooterInfrastructure.Object, mockUserInfrastructure.Object);

        // Act
        var result = await domain.CreateRentAsync(rent);

        // Assert
        Assert.Equal(1, result);
    }
    
    [Fact]
    public void AvailableScooter_WhenCalledWithValidScooterIdAndDates_ReturnsTrue()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        mockRentInfrastructure.Setup(x => x.GetByScooterIdNoAsync(It.IsAny<int>())).Returns(new List<Rent>());
        var domain = new RentDomain(mockRentInfrastructure.Object, mockScooterInfrastructure.Object, mockUserInfrastructure.Object);

        // Act
        var result = domain.AvailableScooter(1, DateTime.Now, DateTime.Now.AddDays(1));

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void GetAvailableScooters_WhenCalledWithValidDates_ReturnsListOfScooters()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var scooters = new List<Scooter> { 
            new Scooter 
            { 
                Id = 1, 
                Name = "Test Scooter",
                Brand = "Test Brand",
                Model = "Test Model",
                Description = "Test Description",
                ImageUrl = "TestImageUrl.jpg"
            } 
        };
    
        mockScooterInfrastructure.Setup(x => x.GetAll()).Returns(scooters);
        mockRentInfrastructure.Setup(x => x.GetByScooterIdNoAsync(It.IsAny<int>())).Returns(new List<Rent>());
        var domain = new RentDomain(mockRentInfrastructure.Object, mockScooterInfrastructure.Object, mockUserInfrastructure.Object);

        // Act
        var result = domain.GetAvailableScooters(DateTime.Now, DateTime.Now.AddDays(1));

        // Assert
        Assert.IsType<List<Scooter>>(result);
        Assert.Single(result);
    }
    
    [Fact]
    public void CancelUnfinishedRent_WhenCalledWithValidRentId_ReturnsTrue()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        mockRentInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        var domain = new RentDomain(mockRentInfrastructure.Object, mockScooterInfrastructure.Object, mockUserInfrastructure.Object);

        // Act
        var result = domain.CancelUnfinishedRent(1);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void CancelUnfinishedRent_WhenCalledWithInvalidRentId_ReturnsException()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        mockRentInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new RentDomain(mockRentInfrastructure.Object, mockScooterInfrastructure.Object, mockUserInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.CancelUnfinishedRent(1));
    }
    

    [Fact]
    public async Task CreateRentAsync_WhenCalledWithInvalidUserId_ThrowsException()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var rent = new Rent
        {
            UserId = -1,
            ScooterId = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)
        };
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new RentDomain(mockRentInfrastructure.Object, mockScooterInfrastructure.Object, mockUserInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.CreateRentAsync(rent));
    }

    [Fact]
    public async Task CreateRentAsync_WhenCalledWithInvalidScooterId_ThrowsException()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var rent = new Rent
        {
            UserId = 1,
            ScooterId = -1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1)
        };
        mockScooterInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new RentDomain(mockRentInfrastructure.Object, mockScooterInfrastructure.Object, mockUserInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.CreateRentAsync(rent));
    }

    [Fact]
    public void AvailableScooter_WhenCalledWithInvalidScooterId_ThrowsException()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        mockRentInfrastructure.Setup(x => x.GetByScooterIdNoAsync(It.IsAny<int>())).Returns(new List<Rent> { new Rent { StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) } });
        var domain = new RentDomain(mockRentInfrastructure.Object, mockScooterInfrastructure.Object, mockUserInfrastructure.Object);

        // Act & Assert
        Assert.False(domain.AvailableScooter(1, DateTime.Now, DateTime.Now.AddDays(1)));
    }

    [Fact]
    public void CancelUnfinishedRent_WhenCalledWithInvalidRentId_ThrowsException()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        mockRentInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new RentDomain(mockRentInfrastructure.Object, mockScooterInfrastructure.Object, mockUserInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.CancelUnfinishedRent(1));
    }
    

    [Fact]
    public void AvailableScooter_WhenCalledWithInvalidDates_ThrowsException()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        mockRentInfrastructure.Setup(x => x.GetByScooterIdNoAsync(It.IsAny<int>())).Returns(new List<Rent> { new Rent { StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(2) } });
        var domain = new RentDomain(mockRentInfrastructure.Object, mockScooterInfrastructure.Object, mockUserInfrastructure.Object);

        // Act & Assert
        Assert.False(domain.AvailableScooter(1, DateTime.Now, DateTime.Now.AddDays(1)));
    }

    [Fact]
    public void GetAvailableScooters_WhenCalledWithInvalidDates_ReturnsEmptyList()
    {
        // Arrange
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var scooters = new List<Scooter> { 
            new Scooter 
            { 
                Id = 1, 
                Name = "Test Scooter",
                Brand = "Test Brand",
                Model = "Test Model",
                Description = "Test Description",
                ImageUrl = "TestImageUrl.jpg"
            } 
        };

        mockScooterInfrastructure.Setup(x => x.GetAll()).Returns(scooters);
        mockRentInfrastructure.Setup(x => x.GetByScooterIdNoAsync(It.IsAny<int>())).Returns(new List<Rent> { new Rent { StartDate = DateTime.Now.AddDays(1), EndDate = DateTime.Now.AddDays(2) } });
        var domain = new RentDomain(mockRentInfrastructure.Object, mockScooterInfrastructure.Object, mockUserInfrastructure.Object);

        // Act
        var result = domain.GetAvailableScooters(DateTime.Now, DateTime.Now.AddDays(1));

        // Assert
        Assert.IsType<List<Scooter>>(result);
        Assert.Empty(result);
    }
    
    
}