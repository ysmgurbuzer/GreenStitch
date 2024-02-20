using APIGreenStitch.Jwt;
using APIGreenStitch.Models;
using BusinessLayer.Abstract;
using Dtos;
using EntityLayer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Net.Http;
using System.Security.Claims;

namespace APIGreenStitch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<Member> _userManager;
        private readonly JwtTokenGenerator _JwtTokenGenerator;
        private readonly IHttpContextAccessor _context;
        private readonly ITokenBlackListService _tokenBlackListService;
      
        public AuthController(UserManager<Member> userManager, 
            JwtTokenGenerator jwtToken, 
            IHttpContextAccessor httpContext,
            ITokenBlackListService tokenBlackListService)
        {
                _userManager = userManager;
                _JwtTokenGenerator = jwtToken;
            _context = httpContext;
            _tokenBlackListService = tokenBlackListService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(MemberCreateDto dto)
        {
            if (ModelState.IsValid)
            {
                var UserExist = await _userManager.FindByEmailAsync(dto.Email);
                if (UserExist != null)
                {
                    return BadRequest("Email already Exist");


                }
                var member = new Member
                {
                    Name = dto.Name,
                    Surname = dto.Surname,
                    UserName = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Address = dto.Address,
                    GenderId = dto.GenderId,
                    Email = dto.Email,
                };

                var IsCreated = await _userManager.CreateAsync(member, dto.Password);

                if (IsCreated.Succeeded)
                {
                    return Ok("User Registration Successful");
                }
            }
            return BadRequest();
        }



        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(MemberLoginDto user)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(user.Email))
                {
                    var existingUser = await _userManager.FindByNameAsync(user.Email);

                    var auth = new MemberLoginDto
                    {
                        Email = existingUser.Email,
                        Id = existingUser.Id,
                    };

                    if (auth == null)
                    {
                        return BadRequest();
                    }

                    var IsCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                    if (IsCorrect)
                    {
                                    
                       
                        string tokenStringNew = _JwtTokenGenerator.GenerateTokenv1(auth);
                        return Ok(tokenStringNew);
                    }
                }
            }

            return Unauthorized();
        }

        [HttpPost("Logout")]

        public IActionResult Logout()
        {
            var token = _context.HttpContext.Request.Headers["Authorization"];

           
            if (!_tokenBlackListService.IsTokenBlacklisted(token))
            {

                _tokenBlackListService.AddToBlacklist(token);


                _context.HttpContext.Session.Clear();

                return Ok("Logged out successfully");
            }

            return Unauthorized("Invalid token");
        }

        [HttpGet]
        public async Task<IActionResult> AuthMemberDetails()
        {
            try
            {
                var userIdClaim = _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdClaim, out int memberId))
                {
                    var member = await _userManager.FindByIdAsync(memberId.ToString());

                    if (member != null)
                    {
                      
                        var memberDetails = new
                        {
                            member.Id,
                            member.Name,
                            member.Surname,
                            member.Email,
                            member.WalletAmount
                          
                        };

                        return Ok(memberDetails);
                    }
                    else
                    {
                        return NotFound("Kullanıcı bulunamadı");
                    }
                }
                else
                {
                    return BadRequest("Geçerli bir kullanıcı ID bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata oluştu: {ex.Message}");
            }
        }



        [HttpPost("UpdateWalletAmount")]
      
        public async Task<IActionResult> UpdateWalletAmount([FromBody] MemberUpdateDto memberUpdateDto)
        {
            try
            {
                var userIdClaim = _context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (int.TryParse(userIdClaim, out int memberId))
                {
                    var member = await _userManager.FindByIdAsync(memberId.ToString());

                    if (member != null)
                    {
                        
                        member.WalletAmount = memberUpdateDto.WalletAmount;

                        var result = await _userManager.UpdateAsync(member);

                        if (result.Succeeded)
                        {
                            var updatedMemberDetails = new
                            {
                               
                                member.WalletAmount
                            };

                            return Ok(updatedMemberDetails);
                        }
                        else
                        {
                            
                            return BadRequest("Wallet amount update failed");
                        }
                    }
                    else
                    {
                        return NotFound("Kullanıcı bulunamadı");
                    }
                }
                else
                {
                    return BadRequest("Geçerli bir kullanıcı ID bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hata oluştu: {ex.Message}");
            }
        }
    }
}
