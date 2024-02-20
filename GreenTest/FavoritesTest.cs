using APIGreenStitch.Controllers;
using BusinessLayer.Abstract;
using CommonLayer.CustomErrorHandler;
using Dtos;
using EntityLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GreenTest
{
    public class FavoritesTest
    {

        public class FavoritesControllerTests
        {
            [Fact]
            public async Task AddFavorites_ValidDto_ReturnsOk()
            {
                // Arrange
                var favoritesServiceMock = new Mock<IFavoritesService>();
                favoritesServiceMock
                    .Setup(x => x.GetByAdvertIdAndMemberIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync((Favorites)null);

                favoritesServiceMock
                    .Setup(x => x.AddAsync(It.IsAny<FavoritesCreateDto>()))
                    .ReturnsAsync(Response<FavoritesCreateDto>.Success(new FavoritesCreateDto()));

                var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
                httpContextAccessorMock
                    .Setup(x => x.HttpContext.User.FindFirst(It.IsAny<string>()))
                    .Returns(new Claim(ClaimTypes.NameIdentifier, "1")); // Updated here to use Claim class

                var controller = new FavoritesController(favoritesServiceMock.Object, httpContextAccessorMock.Object);

                // Act
                var result = await controller.AddFavorites(new FavoritesCreateDto());

                // Assert
                Assert.IsType<OkObjectResult>(result);
            }


            [Fact]
            public async Task ListFavorites_ReturnsOk()
            {
                // Arrange
                var favoritesServiceMock = new Mock<IFavoritesService>();
                favoritesServiceMock
                    .Setup(x => x.ListByMemberIdAsync(It.IsAny<int>()))
                    .ReturnsAsync(Response<List<FavoritesListDto>>.Success(new List<FavoritesListDto>()));

                var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
                httpContextAccessorMock
                    .Setup(x => x.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier))
                    .Returns(new Claim(ClaimTypes.NameIdentifier, "1"));

                var controller = new FavoritesController(favoritesServiceMock.Object, httpContextAccessorMock.Object);

                // Act
                var result = await controller.ListFavorites();

                // Assert
                Assert.IsType<OkObjectResult>(result);
            }


       

        }
    }
}
