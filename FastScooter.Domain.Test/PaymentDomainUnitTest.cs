using Xunit;
using Moq;
using FastScooter.Domain.Domain;
using FastScooter.Infrastructure.Interfaces;
using FastScooter.Infrastructure.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FastScooter.Domain.Test;

public class PaymentDomainUnitTest
{
    [Fact]
    public async Task GetAllByUserId_WhenCalledWithValidUserId_ReturnsListOfPayments()
    {
        // Arrange
        var mockPaymentInfrastructure = new Mock<IPaymentInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var payments = new List<Payment>();
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockPaymentInfrastructure.Setup(x => x.GetByUserId(It.IsAny<int>())).ReturnsAsync(payments);
        var domain = new PaymentDomain(mockPaymentInfrastructure.Object, mockUserInfrastructure.Object, mockRentInfrastructure.Object);

        // Act
        var result = await domain.GetAllByUserId(1);

        // Assert
        Assert.IsType<List<Payment>>(result);
    }
    
    [Fact]
    public async Task GetAllByUserId_WhenCalledWithInvalidUserId_ReturnsException()
    {
        // Arrange
        var mockPaymentInfrastructure = new Mock<IPaymentInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new PaymentDomain(mockPaymentInfrastructure.Object, mockUserInfrastructure.Object, mockRentInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.GetAllByUserId(1));
    }
    
    [Fact]
    public async Task GetAllByRentId_WhenCalledWithValidRentId_ReturnsListOfPayments()
    {
        // Arrange
        var mockPaymentInfrastructure = new Mock<IPaymentInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var payments = new List<Payment>();
        mockRentInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockPaymentInfrastructure.Setup(x => x.GetByRentId(It.IsAny<int>())).ReturnsAsync(payments);
        var domain = new PaymentDomain(mockPaymentInfrastructure.Object, mockUserInfrastructure.Object, mockRentInfrastructure.Object);

        // Act
        var result = await domain.GetAllByRentId(1);

        // Assert
        Assert.IsType<List<Payment>>(result);
    }
    
    [Fact]
    public async Task GetAllByRentId_WhenCalledWithInvalidRentId_ReturnsException()
    {
        // Arrange
        var mockPaymentInfrastructure = new Mock<IPaymentInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        mockRentInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new PaymentDomain(mockPaymentInfrastructure.Object, mockUserInfrastructure.Object, mockRentInfrastructure.Object);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => domain.GetAllByRentId(1));
    }
    
    [Fact]
    public void CreateNewPayment_WhenCalledWithValidPayment_ReturnsTrue()
    {
        // Arrange
        var mockPaymentInfrastructure = new Mock<IPaymentInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var payment = new Payment
        {
            UserId = 1,
            RentId = 1
        };
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockRentInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockPaymentInfrastructure.Setup(x => x.save(It.IsAny<Payment>())).Returns(true);
        var domain = new PaymentDomain(mockPaymentInfrastructure.Object, mockUserInfrastructure.Object, mockRentInfrastructure.Object);

        // Act
        var result = domain.CreateNewPayment(payment);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void RemovePayment_WhenCalledWithValidPaymentId_ReturnsTrue()
    {
        // Arrange
        var mockPaymentInfrastructure = new Mock<IPaymentInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        mockPaymentInfrastructure.Setup(x => x.existsById(It.IsAny<int>())).Returns(true);
        var domain = new PaymentDomain(mockPaymentInfrastructure.Object, mockUserInfrastructure.Object, mockRentInfrastructure.Object);

        // Act
        var result = domain.RemovePayment(1);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void RemovePayment_WhenCalledWithInvalidPaymentId_ReturnsException()
    {
        // Arrange
        var mockPaymentInfrastructure = new Mock<IPaymentInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        mockPaymentInfrastructure.Setup(x => x.existsById(It.IsAny<int>())).Returns(false);
        var domain = new PaymentDomain(mockPaymentInfrastructure.Object, mockUserInfrastructure.Object, mockRentInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.RemovePayment(1));
    }

    [Fact]
    public void CreateNewPayment_WhenCalledWithInvalidUserId_ThrowsException()
    {
        // Arrange
        var mockPaymentInfrastructure = new Mock<IPaymentInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var payment = new Payment
        {
            UserId = -1,
            RentId = 1
        };
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new PaymentDomain(mockPaymentInfrastructure.Object, mockUserInfrastructure.Object, mockRentInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.CreateNewPayment(payment));
    }

    [Fact]
    public void CreateNewPayment_WhenCalledWithInvalidRentId_ThrowsException()
    {
        // Arrange
        var mockPaymentInfrastructure = new Mock<IPaymentInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var payment = new Payment
        {
            UserId = 1,
            RentId = -1
        };
        mockRentInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(false);
        var domain = new PaymentDomain(mockPaymentInfrastructure.Object, mockUserInfrastructure.Object, mockRentInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.CreateNewPayment(payment));
    }
    
    [Fact]
    public void RemovePayment_WhenCalledWithInvalidPaymentId_ThrowsException()
    {
        // Arrange
        var mockPaymentInfrastructure = new Mock<IPaymentInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        mockPaymentInfrastructure.Setup(x => x.existsById(It.IsAny<int>())).Returns(false);
        var domain = new PaymentDomain(mockPaymentInfrastructure.Object, mockUserInfrastructure.Object, mockRentInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.RemovePayment(1));
    }
    
    [Fact]
    public void CreateNewPayment_WhenCalledWithPaymentHavingInvalidAmount_ThrowsException()
    {
        // Arrange
        var mockPaymentInfrastructure = new Mock<IPaymentInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var payment = new Payment
        {
            UserId = 1,
            RentId = 1,
            Amount = -1 // Invalid amount
        };
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockRentInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        var domain = new PaymentDomain(mockPaymentInfrastructure.Object, mockUserInfrastructure.Object, mockRentInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.CreateNewPayment(payment));
    }

    [Fact]
    public void CreateNewPayment_WhenCalledWithPaymentHavingInvalidDate_ThrowsException()
    {
        // Arrange
        var mockPaymentInfrastructure = new Mock<IPaymentInfrastructure>();
        var mockUserInfrastructure = new Mock<IUserInfrastructure>();
        var mockRentInfrastructure = new Mock<IRentInfrastructure>();
        var payment = new Payment
        {
            UserId = 1,
            RentId = 1,
            Amount = 100,
        };
        mockUserInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        mockRentInfrastructure.Setup(x => x.ExistsById(It.IsAny<int>())).Returns(true);
        var domain = new PaymentDomain(mockPaymentInfrastructure.Object, mockUserInfrastructure.Object, mockRentInfrastructure.Object);

        // Act & Assert
        Assert.Throws<Exception>(() => domain.CreateNewPayment(payment));
    }

[Fact]
public void RemovePayment_WhenCalledWithNonExistingPaymentId_ThrowsException()
{
    // Arrange
    var mockPaymentInfrastructure = new Mock<IPaymentInfrastructure>();
    var mockUserInfrastructure = new Mock<IUserInfrastructure>();
    var mockRentInfrastructure = new Mock<IRentInfrastructure>();
    mockPaymentInfrastructure.Setup(x => x.existsById(It.IsAny<int>())).Returns(false);
    var domain = new PaymentDomain(mockPaymentInfrastructure.Object, mockUserInfrastructure.Object, mockRentInfrastructure.Object);

    // Act & Assert
    Assert.Throws<Exception>(() => domain.RemovePayment(-1)); // Non-existing PaymentId
}

        
}