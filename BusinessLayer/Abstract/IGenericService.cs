using CommonLayer.CustomErrorHandler;
using Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<CreateDto, UpdateDto, ListDto, T>
    where T : class
    where CreateDto : class, IDto, new()
    where UpdateDto : class, IUpdateDto, new()
    where ListDto : class, IDto, new()
    {
        Task<Response<ListDto>> GetByIdAsync<IDto>(int id);
        Task<Response<List<ListDto>>> GetListAsync();
        Task<Response<CreateDto>> AddAsync(CreateDto dto);
        Task<Response<T>> RemoveAsync(int id);
        Task<Response<UpdateDto>> UpdateAsync(UpdateDto dto);
        Task<Response<ListDto>> FindAsync<IDto>(int id);
    }
}
