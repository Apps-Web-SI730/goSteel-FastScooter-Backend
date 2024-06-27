using FastScooter.Domain.Domain;
using FastScooter.Infrastructure.Dtos;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;
using Moq;
using Xunit;

namespace FastScooter.Domain.Test;

public class UserDomainUnitTest
{ 
   /* [Fact]
    public async Task CreateUserAsync_WhenCalledWithExistingEmail_ThrowsException()
    {
        // Arrange
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var user = new User()
        {
            Name = "Test User",
            Email = "test@test.com",
            Password = "TestPassword",
            Role = "TestRole" // Add this line
        };
        mockUserInfrastructure.Setup(x => x.ExistsByEmail(It.IsAny<string>())).Returns(true);
        var domain = new UserDomain(mockUserInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.CreateUserAsync(user));
    }

    [Fact]
    public async Task UpdateUserAsync_WhenCalledWithNonExistingId_ThrowsException()
    {
        // Arrange
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
       var userDto = new UserDto()
        {
            Name = "Test User",
            Email = "test@test.com",
            Password = "TestPassword" // Add this line
        };
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new UserDomain(mockUserInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.UpdateUserAsync(1, userDto));
    }

    [Fact]
    public async Task DeleteUserAsync_WhenCalledWithNonExistingId_ThrowsException()
    {
        // Arrange
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new UserDomain(mockUserInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.DeleteUserAsync(1));
    }

    [Fact]
    public void Login_WhenCalledWithInvalidCredentials_ThrowsException()
    {
        // Arrange
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var user = new User()
        {
            Name = "Test User", // Add this line
            Email = "test@test.com",
            Password = "TestPassword",
            Role = "TestRole" // Add this line
        };
        mockUserInfrastructure.Setup(x => x.ExistsByEmailAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
        var domain = new UserDomain(mockUserInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.Login(user));
    }
    
    
        [Fact]
        public void Login_WhenUserInfrastructureGetUserIdByEmailAndPasswordFails_ThrowsException()
        {
            // Arrange
            var mockUserInfrastructure = new Mock<IUserInfrastructure>();
            var user = new User()
            {
                Name = "Test User", // Add this line
                Email = "test@test.com",
                Password = "TestPassword",
                Role = "TestRole" // Add this line
            };
            mockUserInfrastructure.Setup(x => x.ExistsByEmailAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            mockUserInfrastructure.Setup(x => x.GetUserIdByEmailAndPassword(It.IsAny<User>())).Throws<Exception>();
            var domain = new UserDomain(mockUserInfrastructure.Object);

            // Act & Assert
            Assert.Throws<Exception>(() => domain.Login(user));
        }
        
        [Fact]
        public async Task CreateUserAsync_WhenCalledWithInvalidUser_ThrowsException()
        {
            // Arrange
            var mockUserInfrastructure = new Mock<IUserInfrastructure>();
            var user = new User
            {
                Name = "Test User",
                Email = "test@test.com",
                Password = "", // Invalid password
                Role = "TestRole"
            };
            var domain = new UserDomain(mockUserInfrastructure.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => domain.CreateUserAsync(user));
        }

        [Fact]
        public async Task UpdateUserAsync_WhenCalledWithInvalidUserDto_ThrowsException()
        {
            // Arrange
            var mockUserInfrastructure = new Mock<IUserInfrastructure>();
            var userDto = new UserDto()
            {
                Name = new string('a', 51), // Invalid name
                Email = "test@test.com",
                Password = "TestPassword"
            };
            mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
            var domain = new UserDomain(mockUserInfrastructure.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => domain.UpdateUserAsync(1, userDto));
        }*/


}