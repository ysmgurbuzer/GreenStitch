using BusinessLayer.Abstract;
using Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace APIGreenStitch.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RecyclingController : ControllerBase
    {
        private readonly IRecyclingHistoryService _recyclingHistoryService;
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _contextAccessor;
        public RecyclingController(IRecyclingHistoryService recyclingHistoryService, ICategoryService categoryService, IHttpContextAccessor contextAccessor)
        {
            _recyclingHistoryService = recyclingHistoryService;
            _categoryService = categoryService; 
            _contextAccessor = contextAccessor; 
        }

        [HttpPost]
        public async Task<IActionResult> StartRecycling(RecyclingHistoryCreateDto dto)
        {
            var recyclingHistoryResponse = await _recyclingHistoryService.AddAsync(dto);
            if (recyclingHistoryResponse.Succeeded)
            {
                return Ok(recyclingHistoryResponse);

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet]
        public async Task<IActionResult> ListRecycling()
        {
            var userIdClaim = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int memberId)&& userIdClaim!=null)
            {
                var recyclingHistoryResponse = await _recyclingHistoryService.ListByMemberIdAsync(memberId);
                if (recyclingHistoryResponse.Succeeded)
                {
                    return Ok(recyclingHistoryResponse);

                }
                else
                {
                    return BadRequest();
                }

            }
            else
            {
                return BadRequest();    
            }
                
           

        }

       

    }
}
