using APIGreenStitch.Models;
using Braintree;
using BusinessLayer.Abstract;
using Dtos;
using Dtos.CartDtos;
using EntityLayer;
using Hangfire;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIGreenStitch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public const string SessionKey = "cartItems";
        private readonly IBraintreeGateway _braintreeGateway;
        private readonly IHttpContextAccessor _httpContext; 
        private readonly IOrderHistoryService _orderHistoryService;
        private readonly IMemberService _memberService;
        private readonly ICartService _cartService;
        private readonly UserManager<Member> _userManager;

        public PaymentController(IBraintreeGateway braintreeGateway, IHttpContextAccessor httpContext, 
            IOrderHistoryService orderHistory, IMemberService memberService,ICartService cartService, UserManager<Member> userManager)
        {
            _braintreeGateway = braintreeGateway;
            _httpContext = httpContext;   
            _orderHistoryService = orderHistory;
            _memberService = memberService;
            _cartService = cartService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessPayment(PaymentRequest paymentRequest)
        {
            try
            {
                var userIdClaim = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var cart = _httpContext.HttpContext.Session.GetString(SessionKey);

                if (string.IsNullOrEmpty(cart))
                {
                    return BadRequest("Cart is empty.");
                }

                var cartData = JsonConvert.DeserializeObject<CartDtos>(cart);

             

                var transactionRequest = new TransactionRequest
                {
                    Amount = paymentRequest.cartAmount,
                    PaymentMethodNonce = paymentRequest.nonce,
                };

                var transactionResult = _braintreeGateway.Transaction.Sale(transactionRequest);

                if (transactionResult.IsSuccess())
                {
                    foreach (var cartItem in cartData.Items)
                    {
                   
                       
                        

                        if (int.TryParse(userIdClaim, out int memberId))
                        {
                            var IsCouponUsed = _cartService.IsCuponUse;
                            if (IsCouponUsed == true)
                            {
                                var member = await _userManager.FindByIdAsync(memberId.ToString());
                                member.WalletAmount = 0.0m;
                              
                                await _userManager.UpdateAsync(member);
                                Console.WriteLine(member.WalletAmount);
                            }

                            var orderHistoryEntry = new OrderHistoryCreateDto
                            {
                             
                                OrderDate = DateTime.Now,
                                MemberId = memberId,
                                OrderStatus= "Your Order is Preparing",
                                AdvertId=cartItem.AdvertId,
                                

                            };
                            await _cartService.ClearCartSession();

                            var order= await _orderHistoryService.AddAsync(orderHistoryEntry);
                            if (order.Succeeded) { 
                              
                               
                                BackgroundJob.Schedule(() => _orderHistoryService.UpdateOrderStatus(order.Data), TimeSpan.FromMinutes(1));
                            }
                            
                         
                         
                        }
                    }

                    return Ok("Payment successful!");
                }
                else
                {
                    return BadRequest($"Payment failed: {transactionResult.Message}");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing payment: {ex.Message}");
            }

        }
   


    }
}