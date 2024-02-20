using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Security.Claims;

namespace APIGreenStitch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderHistoryController : ControllerBase

    {
        private readonly IOrderHistoryService _orderHistoryService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly GreenContext _greenContext;
        private readonly IMapper _mapper;
        public OrderHistoryController(IOrderHistoryService orderHistoryService, IHttpContextAccessor httpContextAccessor, GreenContext greenContext, IMapper mapper)
        {
                _orderHistoryService = orderHistoryService;
            _httpContext = httpContextAccessor;
            _greenContext = greenContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetByIdOrderHistory()
        {
            var userIdClaim = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userIdClaim, out int memberId))
            {
               
                var orderHistories = await _greenContext.OrderHistories
                    .Where(o => o.MemberId == memberId)
                    .ToListAsync();

                if (orderHistories != null && orderHistories.Any())
                {
                   
                    var orderHistoryDtos = _mapper.Map<List<OrderHistoryListDto>>(orderHistories);

                    return Ok(orderHistoryDtos);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet("formatted-list")]
        public async Task<IActionResult> GetFormattedOrderHistories()
        {
            try
            {
                var userIdClaim = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!int.TryParse(userIdClaim, out int memberId))
                {
                    return BadRequest("Invalid member ID");
                }

                var orderHistories = await _greenContext.OrderHistories
                    .Where(o => o.MemberId == memberId)
                    .ToListAsync();

                if (orderHistories == null)
                {
                    return Ok("No order histories found.");
                }

                var formattedOrderHistories = orderHistories
                    .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month, o.OrderDate.Day, o.OrderDate.Hour, o.OrderDate.Minute })
                    .Select(group => new
                    {
                        Date = $"{group.Key.Year}-{group.Key.Month:D2}-{group.Key.Day:D2} {group.Key.Hour:D2}:{group.Key.Minute:D2}",
                        Orders = group.Select(order => new
                        {
                            AdvertId = order.AdvertId,
                            MemberId = order.MemberId,
                            Status = order.OrderStatus
                        }).ToList()
                    })
                    .ToList();

                return Ok(formattedOrderHistories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving formatted order histories: {ex.Message}");
            }
        }


    }
}
