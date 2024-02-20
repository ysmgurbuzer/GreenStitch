using CommonLayer.CustomErrorHandler;
using Dtos;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOrderHistoryService
    {
        Task<Response<OrderHistoryListDto>> GetByIdAsync<IDto>(int id);
       
        Task<Response<List<OrderHistoryListDto>>> GetListAsync();
        Task<Response<int>> AddAsync(OrderHistoryCreateDto dto);
        Task UpdateOrderStatus(int Id);


    }
}
