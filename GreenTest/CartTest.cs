using BusinessLayer.Abstract;
using CommonLayer.CustomErrorHandler;
using Dtos.CartDtos;
using GreenStitchAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenTest
{
    public class CartTest
    {
        
            private readonly Mock<ICartService> _cartServiceMock;
            private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
            private readonly CartController _cartController;

            public CartTest()
            {
                _cartServiceMock = new Mock<ICartService>();
                _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
                _cartController = new CartController(_cartServiceMock.Object, _httpContextAccessorMock.Object);
            }

        [Fact]
        public async Task AddtoCard_ReturnsOkResult()
        {
            
            var cartServiceMock = new Mock<ICartService>();
            cartServiceMock.Setup(service => service.AddToCartAsync(It.IsAny<int>()))
                .ReturnsAsync(new Response<CartDtos> { Succeeded = true });

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var controller = new CartController(cartServiceMock.Object, httpContextAccessorMock.Object);

            var result = await controller.AddtoCard(1);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetCart_ReturnsOkResult()
        {
            var cartServiceMock = new Mock<ICartService>();
            var mockCartDtos = new CartDtos
            {
                Items = new List<CartDtos.ShoppingCartItem>
                {
                    new CartDtos.ShoppingCartItem { AdvertId = 1, Quantity = 2 }
                }
               
            };
            cartServiceMock.Setup(service => service.GetCartAsync())
                .ReturnsAsync(mockCartDtos);

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var controller = new CartController(cartServiceMock.Object, httpContextAccessorMock.Object);

            var result = await controller.GetCart();

            Assert.IsType<OkObjectResult>(result);
        
        }


        [Fact]
        public async Task RemoveFromCart_ReturnsOkResult()
        {
            
            var cartServiceMock = new Mock<ICartService>();
            cartServiceMock.Setup(service => service.RemoveFromCartAsync(It.IsAny<int>()))
                .ReturnsAsync(new Response<string> { Succeeded = true });

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var controller = new CartController(cartServiceMock.Object, httpContextAccessorMock.Object);

            var result = await controller.RemoveFromCart(1);

            Assert.IsType<OkObjectResult>(result);
     
        }






    }
}
