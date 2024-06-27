using Xunit;
using Moq;
using FastScooter.Domain.Domain;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;
using System.Threading.Tasks;

namespace FastScooter.Domain.Test;
public class ScooterDomainUnitTest
{
    [Fact]
    public async Task CreateScooterAsync_WhenCalledWithValidScooter_ReturnsScooterId()
    {
        // Arrange
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var scooter = new Scooter()
        {
            Name = "Test Scooter",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            Price = 100,
            ImageUrl = "TestImageUrl.jpg" // Add this line
        };
        mockScooterInfrastructure.Setup(x => x.CreateScooterAsync(It.IsAny<Scooter>())).ReturnsAsync(1);
        var domain = new ScooterDomain(mockScooterInfrastructure.Object);

        // Act
        var result = await domain.CreateScooterAsync(scooter);

        // Assert
        Assert.Equal(1, result);
    }
    
    [Fact] 
    public async Task UpdateScooterAsync_WhenCalledWithValidScooterIdAndScooter_ReturnsTrue()
    {
        // Arrange
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var scooter = new Scooter
        {
            Name = "Test Scooter",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            Price = 100,
            ImageUrl = "TestImageUrl.jpg" // Add this line
        };
        mockScooterInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockScooterInfrastructure.Setup(x => x.UpdateScooterAsync(It.IsAny<int>(), It.IsAny<Scooter>())).ReturnsAsync(true);
        var domain = new ScooterDomain(mockScooterInfrastructure.Object);

        // Act
        var result = await domain.UpdateScooterAsync(1, scooter);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task DeleteScooterAsync_WhenCalledWithValidScooterId_ReturnsTrue()
    {
        // Arrange
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        mockScooterInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockScooterInfrastructure.Setup(x => x.DeleteScooterAsync(It.IsAny<int>())).ReturnsAsync(true);
        var domain = new ScooterDomain(mockScooterInfrastructure.Object);

        // Act
        var result = await domain.DeleteScooterAsync(1);

        // Assert
        Assert.True(result);
    } 
    
    /*[Fact]
    public async Task CreateScooterAsync_WhenCalledWithInvalidScooter_ThrowsException()
    {
        // Arrange
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var scooter = new Scooter
        {
            Name = "Test Scooter",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            Price = 100,
            ImageUrl = "TestImageUrl.jpg" // Add this line
        };
        var domain = new ScooterDomain(mockScooterInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.CreateScooterAsync(scooter));
    }*/

    [Fact]
    public async Task UpdateScooterAsync_WhenCalledWithNonExistingId_ThrowsException()
    {
        // Arrange
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var scooter = new Scooter
        {
            Name = "Test Scooter",
            Brand = "Test Brand",
            Model = "Test Model",
            Description = "Test Description",
            Price = 100,
            ImageUrl = "TestImageUrl.jpg" // Add this line
        };
        mockScooterInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new ScooterDomain(mockScooterInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.UpdateScooterAsync(1, scooter));
    }

    [Fact]
    public async Task DeleteScooterAsync_WhenCalledWithNonExistingId_ThrowsException()
    {
        // Arrange
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        mockScooterInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new ScooterDomain(mockScooterInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.DeleteScooterAsync(1));
    }
    
    [Fact]
public async Task CreateScooterAsync_WhenCalledWithInvalidScooter_ThrowsException()
{
    // Arrange
    var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
    var scooter = new Scooter
    {
        Name = "Test Scooter",
        Brand = "Test Brand",
        Model = "Test Model",
        Description = "Test Description",
        Price = -1, // Invalid price
        ImageUrl = "TestImageUrl.jpg"
    };
    var domain = new ScooterDomain(mockScooterInfrastructure.Object);

    // Act & Assert
    await Assert.ThrowsAsync<Exception>(() => domain.CreateScooterAsync(scooter));
}


}