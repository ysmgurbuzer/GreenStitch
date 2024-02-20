using CommonLayer.CustomErrorHandler;
using Dtos;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRecyclingHistoryService
    {
        Task<Response<RecyclingHistoryListDto>> GetByIdAsync<IDto>(int id);
        Task<Response<List<RecyclingHistoryListDto>>> GetListAsync();
        Task<Response<RecyclingHistoryCreateDto>> AddAsync(RecyclingHistoryCreateDto dto);
       int  GetAuthenticatedMember();
        Task<List<RecyclingHistory>> GetListAsync(Expression<Func<RecyclingHistory, bool>> predicate);
        Task<Response<List<RecyclingHistoryListDto>>> ListByMemberIdAsync(int memberId);
    }
}
