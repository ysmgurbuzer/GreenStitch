using BusinessLayer.Abstract;
using Dtos.CartDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GreenStitchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContext;
        public CartController(ICartService cartService, IHttpContextAccessor httpContext)
        {
            _cartService = cartService;
            _httpContext = httpContext;
        }
        [HttpPost("IncrementQuantity/{id}")]
        public async Task<IActionResult> IncrementQuantity(int id)
        {
            var Quantity = await _cartService.StockControl(id);
            if (Quantity.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }


        }


        [HttpPost("{id}")]
        public async Task<IActionResult> AddtoCard(int id)
        {
            try
            {



                var Item = await _cartService.AddToCartAsync(id);
                if (Item.Succeeded)
                {
                    return Ok(Item);
                }
                else { return BadRequest(); }

            }
            catch (Exception ex)
            {
             
                Console.WriteLine($"Hata Oluştu: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return BadRequest("Ürün sepete eklenirken bir hata oluştu.");
            }
        }




        [HttpGet]
        public async Task<IActionResult> GetCart()
        {


            var cart = await _cartService.GetCartAsync();

            if (cart == null)
            {
                return Ok(cart);

            }
            else
            {
               

                return Ok(cart);
            }
        }

        [HttpPost("ReceiveCartItems")]
        public async Task<IActionResult> ReceiveCartItems([FromBody] List<List<CartDtos.ShoppingCartItem>> cartItems)
        {
            try
            {
                if (cartItems != null && cartItems.Any())
                {


                    return Ok();
                }
                else
                {
                    return BadRequest("Invalid cart items format.");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Error processing cart items: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var result = await _cartService.RemoveFromCartAsync(id);

            if (result.Succeeded)
            {
                return Ok("Ürün sepetten silindi.");
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        [HttpGet("UseCoupon")]
        public async Task<IActionResult> UseCoupon()
        {
          var total=  await  _cartService.Cupon();
            return Ok(total);

        }





    }



}