using AutoMapper;
using FastScooter.API.Controllers;
using FastScooter.API.Request;
using FastScooter.API.Response;
using FastScooter.Domain.Interfaces;
using FastScooter.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace FastScooter.API.Test;

public class PaymentControllerUnitTest
{
    [Fact]
    public async Task GetAll_ReturnsOkResult()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockPaymentDomain = new Mock<IPaymentDomain>();
        var mockPaymentResponse = new List<PaymentResponse>();
        var mockPaymentRequest = new PaymentRequest();
        var mockPayment = new Payment();
        var mockPaymentList = new List<Payment>();
        var mockUserId = 1; // Add this line
        mockPaymentDomain.Setup(x => x.GetAllByUserId(mockUserId)).ReturnsAsync(mockPaymentList); // Modify this line
        mockMapper.Setup(x => x.Map<List<PaymentResponse>>(mockPaymentList)).Returns(mockPaymentResponse);
        var controller = new PaymentController(mockPaymentDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAllByUserId(mockUserId); // Corrected this line

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<List<PaymentResponse>>(okResult.Value);
        Assert.Equal(mockPaymentResponse, model);
    }
    
    [Fact]
    public async Task GetAllByScooterId_ReturnsOkResult()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockPaymentDomain = new Mock<IPaymentDomain>();
        var mockPaymentResponse = new List<PaymentResponse>();
        var mockPaymentRequest = new PaymentRequest();
        var mockPayment = new Payment();
        var mockPaymentList = new List<Payment>();
        var mockScooterId = 1; // Add this line
        mockPaymentDomain.Setup(x => x.GetAllByRentId(mockScooterId)).ReturnsAsync(mockPaymentList); // Modify this line
        mockMapper.Setup(x => x.Map<List<PaymentResponse>>(mockPaymentList)).Returns(mockPaymentResponse);
        var controller = new PaymentController(mockPaymentDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAllByRentId(mockScooterId); // Corrected this line

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<List<PaymentResponse>>(okResult.Value);
        Assert.Equal(mockPaymentResponse, model);
    }
    
    [Fact]
    public void Post_ReturnsExpectedResult()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockPaymentDomain = new Mock<IPaymentDomain>();
        var paymentRequest = new PaymentRequest();
        var payment = new Payment();
        mockPaymentDomain.Setup(x => x.CreateNewPayment(It.IsAny<Payment>())).Returns(true);
        mockMapper.Setup(x => x.Map<PaymentRequest, Payment>(It.IsAny<PaymentRequest>())).Returns(payment);
        var controller = new PaymentController(mockPaymentDomain.Object, mockMapper.Object);

        // Act
        var result = controller.Post(paymentRequest);

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void Post_WhenCalled_ReturnsTrue()
    {
        // Arrange
        var mockPaymentDomain = new Mock<IPaymentDomain>();
        var mockMapper = new Mock<IMapper>();
        var paymentRequest = new PaymentRequest();
        var payment = new Payment();
        mockMapper.Setup(x => x.Map<PaymentRequest, Payment>(It.IsAny<PaymentRequest>())).Returns(payment);
        mockPaymentDomain.Setup(x => x.CreateNewPayment(It.IsAny<Payment>())).Returns(true);
        var controller = new PaymentController(mockPaymentDomain.Object, mockMapper.Object);

        // Act
        var result = controller.Post(paymentRequest);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetAllByUserId_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockPaymentDomain = new Mock<IPaymentDomain>();
        var mockMapper = new Mock<IMapper>();
        var payments = new List<Payment>();
        var paymentResponses = new List<PaymentResponse>();
        mockPaymentDomain.Setup(x => x.GetAllByUserId(It.IsAny<int>())).ReturnsAsync(payments);
        mockMapper.Setup(x => x.Map<List<PaymentResponse>>(It.IsAny<List<Payment>>())).Returns(paymentResponses);
        var controller = new PaymentController(mockPaymentDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAllByUserId(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<PaymentResponse>>(okResult.Value);
        Assert.Equal(paymentResponses, returnValue);
    }

    [Fact]
    public async Task GetAllByRentId_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockPaymentDomain = new Mock<IPaymentDomain>();
        var mockMapper = new Mock<IMapper>();
        var payments = new List<Payment>();
        var paymentResponses = new List<PaymentResponse>();
        mockPaymentDomain.Setup(x => x.GetAllByRentId(It.IsAny<int>())).ReturnsAsync(payments);
        mockMapper.Setup(x => x.Map<List<PaymentResponse>>(It.IsAny<List<Payment>>())).Returns(paymentResponses);
        var controller = new PaymentController(mockPaymentDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAllByRentId(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<PaymentResponse>>(okResult.Value);
        Assert.Equal(paymentResponses, returnValue);
    }
    
}