using BusinessLayer.Abstract;
using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;

namespace APIGreenStitch.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
   
	
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoritesService _favoritesService;
        private readonly IHttpContextAccessor _httpContext;
        public FavoritesController(IFavoritesService favoritesService, IHttpContextAccessor httpContext)
        {
            _favoritesService = favoritesService;
            _httpContext = httpContext;
                
        }
        [HttpPost]
        public async Task<IActionResult> AddFavorites(FavoritesCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest("No valid data found to add a favorite.");
            }

            var userIdClaim = _httpContext?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int memberId))
            {
                return BadRequest("A valid user ID was not found.");
            }

            dto.MemberId = memberId;

            var existingFavorite = await _favoritesService.GetByAdvertIdAndMemberIdAsync(dto.AdvertId, memberId);

            if (existingFavorite != null)
            {
                return BadRequest("You have already added this product to favorites.");
            }

            var result = await _favoritesService.AddAsync(dto);

            if (result.Succeeded)
            {
                return Ok(new { Message = "Favorite added successfully" });
            }
            else
            {
                return BadRequest(new { ErrorMessage = result.Message });
            }
        }



        [HttpGet]
        public async Task<IActionResult> ListFavorites()
        {
            var userIdClaim = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int memberId))
            {
                var favorites = await _favoritesService.ListByMemberIdAsync(memberId);
                return Ok(favorites);
            }
            else
            {
                return BadRequest("Geçerli bir kullanıcı ID bulunamadı.");
            }
        }

        [HttpDelete("{advertId}")]
        public async Task<IActionResult> RemoveFavorites(int advertId)
        {
            var userIdClaim = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int memberId))
            {
                var result = await _favoritesService.RemoveFavAsync(memberId, advertId);

                if (result.Succeeded)
                {
                    return Ok("Favorite removed successfully");
                }
                else
                {
                    return BadRequest(result.Message);
                }
            }
            else
            {
                return BadRequest("A valid user ID was not found.");
            }
        }

    }
}
