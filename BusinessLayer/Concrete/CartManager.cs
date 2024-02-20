using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.UnitOfWork;
using Dtos.Abstract;
using Dtos;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using CommonLayer.CustomErrorHandler;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Security.Claims;
using Dtos.CartDtos;

namespace BusinessLayer.Concrete
{
    public class CartManager : ICartService
    {
        private readonly IProductService _productService;
        private readonly IAdvertisementService _advertisementService;
        private readonly GreenContext _context;
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public const string SessionKey = "cartItems";
        private readonly IHttpContextAccessor _httpContext;
        private readonly ICategoryService _categoryService;
        private readonly List<CartDtos.ShoppingCartItem> _carts;
        private readonly IMemberService _memberService; 
        private static bool IsCuponUse;
        public bool _IsCuponUse
        {
            get { return IsCuponUse; }
        }

        bool ICartService.IsCuponUse => _IsCuponUse;

        public CartManager(
            IProductService productService,
            IMapper mapper,
            IUow uow,
            GreenContext context,
            IAdvertisementService advertService,
            IHttpContextAccessor httpContext,
            ICategoryService categoryService,
            IMemberService memberService

            )
        {
            _productService = productService;
            _advertisementService = advertService;
            _context = context;
            _uow = uow;
            _mapper = mapper;
            _carts = new List<CartDtos.ShoppingCartItem>();
            _httpContext = httpContext;
            _categoryService= categoryService;  
            _memberService = memberService;

        }
        public async Task<CartDtos> GetCartAsync()
        {
            var session = _httpContext.HttpContext.Session;
            var userIdClaim = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (UserIsLoggedIn())
            {
                var data = session.GetString(SessionKey);

                if (data != null)
                {
                    var cart = JsonConvert.DeserializeObject<CartDtos>(data);

                    if (cart != null && cart.Items != null && cart.Items.Any())
                    {
                        decimal totalCategoryPrice = cart.Items
                            .Where(item => item.CategoryId == 3)
                            .Sum(item => item.Price * item.Quantity);

                        if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int userId))
                        {
                            var walletResponse = await _memberService.FindAsync<MemberListDto>(userId);
                            var walletMapp = _mapper.Map<MemberListDto>(walletResponse.Data);
                            var walletAmount = walletMapp?.WalletAmount ?? 0;

                            if (totalCategoryPrice >= walletAmount)
                            {
                                totalCategoryPrice -= walletAmount;
                            }

                            if (IsCuponUse)
                            {
                                decimal totalAmountWithCoupon = CalculateTotalAmountWithCupon(cart);
                                cart.TotalAmount = totalAmountWithCoupon + totalCategoryPrice;
                            }
                            else
                            {
                                cart.TotalAmount = CalculateTotalAmount(cart);
                            }

                            Console.WriteLine(cart.TotalAmount);

                            await SaveCartAsync(cart);
                        }
                    }
                    return cart;
                }
                else
                {
                    var newCart = new CartDtos();
                    await SaveCartAsync(newCart);
                    return newCart;
                }
            }

            return null;
        }


        public async Task<decimal> Cupon()
        {
            IsCuponUse = true;

            var cart = await GetCartAsync();

            return cart.TotalAmount;
            
        }



        private bool UserIsLoggedIn()
        {
            return _httpContext.HttpContext.User.Identity.IsAuthenticated;
        }
        public async Task<Response<CartDtos>> AddToCartAsync(int id)
        {
           
            var advertisementResponse = await _advertisementService.GetByIdAsync<AdvertisementListDto>(id);

            var productResponse = await _productService.GetByIdAsync<ProductListDto>(advertisementResponse.Data.AdvertId);

           
            //var productSizeResponse = await _uow.GetGenericDal<ProductSize>().GetByIdAsync(productResponse.Data.ProductId);

            if (advertisementResponse != null)
            {
                var cartDtos = _mapper.Map<CartDtos.ShoppingCartItem>(advertisementResponse.Data);
                var productMap = _mapper.Map<CartDtos.ShoppingCartItem>(productResponse.Data);
            

                var cart = await GetCartAsync();
                var cartLine = new CartDtos.ShoppingCartItem
                {
                    ProductId = productResponse.Data.ProductId,
                    AdvertId = advertisementResponse.Data.AdvertId,
                    ImagePathOne = advertisementResponse.Data.ImagePathOne,
                    Price = productResponse.Data.Price,
                    AdvertTitle = advertisementResponse.Data.AdvertTitle,
                    CategoryId = advertisementResponse.Data.CategoryId,
                    Quantity = 1
                };

                if (cart != null && cartLine != null)
                {
                   
                    cart.Items.Add(cartLine);

                     SaveCartAsync(cart);
                   
                    return Response<CartDtos>.Success(cart);
                }
                else
                {
                   
                    Console.WriteLine("Hata oluştu.");
                    return Response<CartDtos>.Fail("Ürün sepete eklenirken bir hata oluştu.");
                }

              
            }
            return Response<CartDtos>.Fail("Ürün bulunamadı.");
        }
        public async Task<Response<string>> StockControl(int id)
        {
            try
            {
                var cart = await GetCartAsync();
                var cartLine = cart.Items.FirstOrDefault(x => x.AdvertId == id);
                if (cartLine != null)
                {
                    var productSize = await _uow.GetGenericDal<ProductSize>().GetByIdAsync(cartLine.ProductId);

                    if (productSize != null && cartLine.Quantity <= productSize.Stock)
                    {
                       
                        cartLine.Quantity++;
                       

                        await SaveCartAsync(cart);
                        return Response<string>.Success("ürün miktarı artırıldı");
                    }
                    else
                    {
                        Console.WriteLine("Yeterli stok miktarı bulunmamaktadır.");
                        return Response<string>.Fail("Yeterli stok miktarı bulunmamaktadır.");
                    }
                }
                else
                {
                   
                    Console.WriteLine("Ürün bulunamadı.");
                    return Response<string>.Fail("Ürün bulunamadı..");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"AddToCartAsync veya StockControl Hatası: {ex.Message}");
                Console.WriteLine(ex.StackTrace); 
                throw; 
            }


        }
        public async Task SaveCartAsync(CartDtos cart)
        {
            try
            {
                if (_httpContext != null && _httpContext.HttpContext != null && _httpContext.HttpContext.Session != null)
                {
                    var session = _httpContext.HttpContext.Session;

                    if (UserIsLoggedIn())
                    {
                        var cartJson = JsonConvert.SerializeObject(cart);
                        session.SetString(SessionKey, cartJson); 
                        await session.CommitAsync();
                        Console.WriteLine(session.GetString(SessionKey));
                    }
                }
                else
                {
                    Console.WriteLine("HttpContext veya Session null, SaveCartAsync işlemi gerçekleştirilemedi.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SaveCartAsync Hatası: {ex.Message}");
                Console.WriteLine(ex.StackTrace); 
                throw; 
            }
        }


        public async Task<Response<string>> RemoveFromCartAsync(int advertId)
        {
            var cart = await GetCartAsync();
            var cartItemToRemove = cart.Items.FirstOrDefault(item => item.AdvertId == advertId);

            if (cartItemToRemove != null)
            {
              
                cart.Items.Remove(cartItemToRemove);
                await SaveCartAsync(cart);

                return Response<string>.Success("Ürün sepetten çıkarıldı.");
            }
            else
            {
                return Response<string>.Fail("Ürün sepetinizde bulunamadı.");
            }
        }

        public decimal CalculateTotalAmountWithCupon(CartDtos cart)
        {
            return cart.Items?.Where(item => item.CategoryId != 3).Sum(item => item.Price * item.Quantity) ?? 0;
        }

        public decimal CalculateTotalAmount(CartDtos cart)
        {
            return cart.Items?.Sum(item => item.Price * item.Quantity) ?? 0;
        }



        private async Task<Response<ProductListDto>> GetProductDtoAsync(int productId)
        {
            var productResponse = await _productService.GetByIdAsync<ProductListDto>(productId);

            if (productResponse.Succeeded)
            {
                return Response<ProductListDto>.Success(productResponse.Data);
            }

            return Response<ProductListDto>.Fail("Ürün bilgileri alınamadı.");
        }


        public async Task ClearCartSession()
        {
            try
            {
                if (_httpContext != null && _httpContext.HttpContext != null && _httpContext.HttpContext.Session != null)
                {
                    var session = _httpContext.HttpContext.Session;

                    if (UserIsLoggedIn())
                    {
                        session.Remove(SessionKey);
                        await session.CommitAsync();
                        Console.WriteLine("Cart session cleared.");
                    }
                }
                else
                {
                    Console.WriteLine("HttpContext or Session is null. Unable to clear cart session.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ClearCartSession Error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }



    }
}
