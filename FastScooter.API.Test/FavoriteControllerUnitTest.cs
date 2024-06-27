using System.Net;
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


public class FavoriteControllerUnitTest
{
    [Fact]
    public async Task GetAllByUserId_ReturnsOkResult()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockFavoriteDomain = new Mock<IFavoriteDomain>();
        var mockFavoriteResponse = new List<FavoriteResponse>();
        var mockFavoriteRequest = new FavoriteRequest();
        var mockFavorite = new Favorites();
        var mockFavoriteList = new List<Favorites>();
        var mockUserId = 1;
        mockFavoriteDomain.Setup(x => x.GetAllByUserId(mockUserId)).ReturnsAsync(mockFavoriteList);
        mockMapper.Setup(x => x.Map<List<FavoriteResponse>>(mockFavoriteList)).Returns(mockFavoriteResponse);
        var controller = new FavoriteController(mockFavoriteDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAllByUserId(mockUserId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<List<FavoriteResponse>>(okResult.Value);
        Assert.Equal(mockFavoriteResponse, model);
    }

    [Fact]
    public async Task GetAllByScooterId_ReturnsOkResult()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockFavoriteDomain = new Mock<IFavoriteDomain>();
        var mockFavoriteResponse = new List<FavoriteResponse>();
        var mockFavoriteRequest = new FavoriteRequest();
        var mockFavorite = new Favorites();
        var mockFavoriteList = new List<Favorites>();
        var mockScooterId = 1;
        mockFavoriteDomain.Setup(x => x.GetAllByScooterId(mockScooterId)).ReturnsAsync(mockFavoriteList);
        mockMapper.Setup(x => x.Map<List<FavoriteResponse>>(mockFavoriteList)).Returns(mockFavoriteResponse);
        var controller = new FavoriteController(mockFavoriteDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAllByScooterId(mockScooterId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var model = Assert.IsAssignableFrom<List<FavoriteResponse>>(okResult.Value);
        Assert.Equal(mockFavoriteResponse, model);
    }

    [Fact]
    public void Post_ReturnsTrue()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockFavoriteDomain = new Mock<IFavoriteDomain>();
        var mockFavoriteRequest = new FavoriteRequest();
        var mockFavorite = new Favorites();
        mockMapper.Setup(x => x.Map<FavoriteRequest, Favorites>(mockFavoriteRequest)).Returns(mockFavorite);
        mockFavoriteDomain.Setup(x => x.CreateNewFavorite(mockFavorite)).Returns(true);
        var controller = new FavoriteController(mockFavoriteDomain.Object, mockMapper.Object);

        // Act
        var result = controller.Post(mockFavoriteRequest);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task Delete_ReturnsInternalServerError()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockFavoriteDomain = new Mock<IFavoriteDomain>();
        var mockFavoriteRequest = new FavoriteRequest();
        var mockFavorite = new Favorites();
        var mockFavoriteList = new List<Favorites>();
        var mockId = 1;
        mockFavoriteDomain.Setup(x => x.CancelFavorite(mockId)).Throws(new Exception());
        var controller = new FavoriteController(mockFavoriteDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Delete(mockId);

        // Assert
        var internalServerErrorResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, internalServerErrorResult.StatusCode);
    }

    [Fact]
    public async Task GetAllByUserId_ReturnsInternalServerError()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockFavoriteDomain = new Mock<IFavoriteDomain>();
        var mockFavoriteResponse = new List<FavoriteResponse>();
        var mockFavoriteRequest = new FavoriteRequest();
        var mockFavorite = new Favorites();
        var mockFavoriteList = new List<Favorites>();
        var mockUserId = 1;
        mockFavoriteDomain.Setup(x => x.GetAllByUserId(mockUserId)).Throws(new Exception());
        var controller = new FavoriteController(mockFavoriteDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAllByUserId(mockUserId);

        // Assert
        var internalServerErrorResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, internalServerErrorResult.StatusCode);
    }

    [Fact]
    public async Task GetAllByScooterId_ReturnsInternalServerError()
    {
        // Arrange
        var mockMapper = new Mock<IMapper>();
        var mockFavoriteDomain = new Mock<IFavoriteDomain>();
        var mockFavoriteResponse = new List<FavoriteResponse>();
        var mockFavoriteRequest = new FavoriteRequest();
        var mockFavorite = new Favorites();
        var mockFavoriteList = new List<Favorites>();
        var mockScooterId = 1;
        mockFavoriteDomain.Setup(x => x.GetAllByScooterId(mockScooterId)).Throws(new Exception());
        var controller = new FavoriteController(mockFavoriteDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAllByScooterId(mockScooterId);

        // Assert
        var internalServerErrorResult = Assert.IsType<ObjectResult>(result.Result);
        Assert.Equal(500, internalServerErrorResult.StatusCode);
    }

    [Fact]
    public async Task Post_WhenCalled_ReturnsTrue()
    {
        // Arrange
        var mockFavoriteDomain = new Mock<IFavoriteDomain>();
        var mockMapper = new Mock<IMapper>();
        var favoriteRequest = new FavoriteRequest();
        var favorite = new Favorites();
        mockMapper.Setup(x => x.Map<FavoriteRequest, Favorites>(It.IsAny<FavoriteRequest>())).Returns(favorite);
        mockFavoriteDomain.Setup(x => x.CreateNewFavorite(It.IsAny<Favorites>())).Returns(true);
        var controller = new FavoriteController(mockFavoriteDomain.Object, mockMapper.Object);

        // Act
        var result = controller.Post(favoriteRequest);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetAllByUserId_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockFavoriteDomain = new Mock<IFavoriteDomain>();
        var mockMapper = new Mock<IMapper>();
        var favorites = new List<Favorites>();
        var favoriteResponses = new List<FavoriteResponse>();
        mockFavoriteDomain.Setup(x => x.GetAllByUserId(It.IsAny<int>())).ReturnsAsync(favorites);
        mockMapper.Setup(x => x.Map<List<FavoriteResponse>>(It.IsAny<List<Favorites>>())).Returns(favoriteResponses);
        var controller = new FavoriteController(mockFavoriteDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAllByUserId(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<FavoriteResponse>>(okResult.Value);
        Assert.Equal(favoriteResponses, returnValue);
    }

    [Fact]
    public async Task GetAllByScooterId_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockFavoriteDomain = new Mock<IFavoriteDomain>();
        var mockMapper = new Mock<IMapper>();
        var favorites = new List<Favorites>();
        var favoriteResponses = new List<FavoriteResponse>();
        mockFavoriteDomain.Setup(x => x.GetAllByScooterId(It.IsAny<int>())).ReturnsAsync(favorites);
        mockMapper.Setup(x => x.Map<List<FavoriteResponse>>(It.IsAny<List<Favorites>>())).Returns(favoriteResponses);
        var controller = new FavoriteController(mockFavoriteDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAllByScooterId(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<FavoriteResponse>>(okResult.Value);
        Assert.Equal(favoriteResponses, returnValue);
    }

    [Fact]
    public async Task Delete_WhenCalled_ReturnsOkResult()
    {
        // Arrange
        var mockFavoriteDomain = new Mock<IFavoriteDomain>();
        var mockMapper = new Mock<IMapper>();
        mockFavoriteDomain.Setup(x => x.CancelFavorite(It.IsAny<int>())).ReturnsAsync(true);
        var controller = new FavoriteController(mockFavoriteDomain.Object, mockMapper.Object);

        // Act
        var result = await controller.Delete(1);

        // Assert
        var okResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
    }

}