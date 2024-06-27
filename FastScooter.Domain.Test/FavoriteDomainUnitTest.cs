using FastScooter.Domain.Domain;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;
using Moq;
using Xunit;

namespace FastScooter.Domain.Test;

public class FavoriteDomainUnitTest
{
    [Fact]
    public void CreateNewFavorite_WhenCalledWithValidFavorite_ReturnsTrue()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var favorite = new Favorites
        {
            UserId = 1,
            ScooterId = 1
        };
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockScooterInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockFavoriteInfrastructure.Setup(x => x.save(It.IsAny<Favorites>())).Returns(true);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act
        var result = domain.CreateNewFavorite(favorite);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void CreateNewFavorite_WhenCalledWithInvalidUser_ReturnsException()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var favorite = new Favorites
        {
            UserId = 1,
            ScooterId = 1
        };
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.CreateNewFavorite(favorite));
    }
    
    [Fact]
    public void CreateNewFavorite_WhenCalledWithInvalidScooter_ReturnsException()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var favorite = new Favorites
        {
            UserId = 1,
            ScooterId = 1
        };
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockScooterInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.CreateNewFavorite(favorite));
    }
    
    /*[Fact]
    public void CreateNewFavorite_WhenCalledWithInvalidFavorite_ReturnsException()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var favorite = new Favorites
        {
            UserId = 1,
            ScooterId = 1
        };
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockScooterInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockFavoriteInfrastructure.Setup(x => x.save(It.IsAny<Favorites>())).Returns(false);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.CreateNewFavorite(favorite));
    }*/
    
    [Fact]
    public void RemoveFavorite_WhenCalledWithValidFavorite_ReturnsTrue()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var favorite = new Favorites
        {
            UserId = 1,
            ScooterId = 1
        };
        mockFavoriteInfrastructure.Setup(x => x.existsById(It.IsAny<int>())).Returns(true);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act
        var result = domain.RemoveFavorite(1);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void RemoveFavorite_WhenCalledWithInvalidFavorite_ReturnsException()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var favorite = new Favorites
        {
            UserId = 1,
            ScooterId = 1
        };
        mockFavoriteInfrastructure.Setup(x => x.existsById(It.IsAny<int>())).Returns(false);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.RemoveFavorite(1));
    }
    
    [Fact]
    public void GetAllByUserId_WhenCalledWithValidUserId_ReturnsListOfFavorites()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var favorites = new List<Favorites>();
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockFavoriteInfrastructure.Setup(x => x.GetByUserId(It.IsAny<int>())).ReturnsAsync(favorites);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act
        var result = domain.GetAllByUserId(1).Result;

        // Assert
        Assert.IsType<List<Favorites>>(result);
    }
    
    [Fact]
    public async Task GetAllByUserId_WhenCalledWithInvalidUserId_ReturnsException()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var favorites = new List<Favorites>();
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.GetAllByUserId(1));
    }

/*[Fact]
public async Task GetAllByScooterId_WhenCalledWithValidScooterId_ReturnsListOfFavorites()
{
    // Arrange
    var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
    var mockUserInfrastructure = new Mock<IUserInfrastructure>();
    var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
    var favorites = new List<Favorites>();
    mockScooterInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
    mockFavoriteInfrastructure.Setup(x => x.GetByUserId(It.IsAny<int>())).ReturnsAsync(favorites);
    var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

    // Act
    var result = await domain.GetAllByScooterId(1);

    // Assert
    Assert.IsType<List<Favorites>>(result);
}*/

    [Fact]
    public async Task GetAllByScooterId_WhenCalledWithInvalidScooterId_ReturnsException()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        mockScooterInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.GetAllByScooterId(1));
    }

    [Fact]
    public async Task CancelFavorite_WhenCalledWithValidFavoriteId_ReturnsTrue()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        mockFavoriteInfrastructure.Setup(x => x.existsById(It.IsAny<int>())).Returns(true);
        mockFavoriteInfrastructure.Setup(x => x.DeleteUserAsync(It.IsAny<int>())).ReturnsAsync(true);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act
        var result = await domain.CancelFavorite(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CancelFavorite_WhenCalledWithInvalidFavoriteId_ReturnsException()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        mockFavoriteInfrastructure.Setup(x => x.existsById(It.IsAny<int>())).Returns(false);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.CancelFavorite(1));
    }
    
    [Fact]
    public void CreateNewFavorite_WhenCalledWithNull_ThrowsException()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => domain.CreateNewFavorite(null));
    }

    [Fact]
    public void RemoveFavorite_WhenCalledWithInvalidId_ThrowsException()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        mockFavoriteInfrastructure.Setup(x => x.existsById(It.IsAny<int>())).Returns(false);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.RemoveFavorite(-1));
    }

    [Fact]
    public async Task GetAllByUserId_WhenCalledWithInvalidUserId_ThrowsException()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.GetAllByUserId(-1));
    }

    [Fact]
    public async Task GetAllByScooterId_WhenCalledWithInvalidScooterId_ThrowsException()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        mockScooterInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.GetAllByScooterId(-1));
    }

    [Fact]
    public async Task CancelFavorite_WhenCalledWithInvalidFavoriteId_ThrowsException()
    {
        // Arrange
        var mockFavoriteInfrastructure = new Mock<IFavoriteInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockScooterInfrastructure = new Mock<IScooterInfrastructure>();
        mockFavoriteInfrastructure.Setup(x => x.existsById(It.IsAny<int>())).Returns(false);
        var domain = new FavoriteDomain(mockFavoriteInfrastructure.Object, mockUserInfrastructure.Object, mockScooterInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.CancelFavorite(-1));
    }
    
    
    
}