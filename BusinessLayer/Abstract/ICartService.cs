using CommonLayer.CustomErrorHandler;
using Dtos;
using Dtos.CartDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ICartService
    {
        Task ClearCartSession();
        Task<CartDtos> GetCartAsync();
        Task<decimal> Cupon();
        bool IsCuponUse { get; }
        Task<Response<CartDtos>> AddToCartAsync(int id);
        Task<Response<string>> StockControl(int id);
        Task SaveCartAsync(CartDtos cart);
        Task<Response<string>> RemoveFromCartAsync(int Id);
        decimal CalculateTotalAmount(CartDtos cart);
        decimal CalculateTotalAmountWithCupon(CartDtos cart);
      
    }
}