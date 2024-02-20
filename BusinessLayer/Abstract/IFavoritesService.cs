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
    public interface IFavoritesService:IGenericService<FavoritesCreateDto, FavoritesUpdateDto, FavoritesListDto, Favorites>
    {
        Task<Response<List<FavoritesListDto>>> ListByMemberIdAsync(int memberId);
        Task<List<Favorites>> GetListAsync(Expression<Func<Favorites, bool>> predicate);
        Task<Response<Favorites>> RemoveFavAsync(int memberId, int advertId);
        Task<Favorites> GetByAdvertIdAndMemberIdAsync(int advertId, int memberId);
    }
}
