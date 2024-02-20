using AutoMapper;
using BusinessLayer.Abstract;
using CommonLayer.CustomErrorHandler;
using DataAccessLayer.Context;
using DataAccessLayer.UnitOfWork;
using Dtos;
using EntityLayer;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class FavoritesManager : GenericManager<FavoritesCreateDto, FavoritesUpdateDto, FavoritesListDto, Favorites>, IFavoritesService
    {
        private const string FavoritesSessionKey = "Favorites";
        private readonly GreenContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IUow _uow;
        public FavoritesManager(IMapper mapper,
            IValidator<FavoritesCreateDto> createDto,
             IValidator<FavoritesUpdateDto> updateDto,
            IValidator<FavoritesListDto> listDto,
           IUow uow, IHttpContextAccessor httpContext,  
           GreenContext context) : base(mapper, createDto, listDto, updateDto, uow)
        {
            _httpContextAccessor = httpContext;
            _context = context;
            _mapper = mapper;
            _uow = uow;
        }

       
        public async Task<Response<List<FavoritesListDto>>> ListByMemberIdAsync(int memberId)
        {
            var favoritesData = await GetListAsync(f => f.MemberId == memberId);

            if (favoritesData == null)
            {
                return Response<List<FavoritesListDto>>.Fail("Favori bulunamadı");
            }

            var favoritesDto = _mapper.Map<List<FavoritesListDto>>(favoritesData);
            return Response<List<FavoritesListDto>>.Success(favoritesDto);
        }


        public async Task<List<Favorites>> GetListAsync(Expression<Func<Favorites, bool>> predicate)
        {
            return await _context.Set<Favorites>().Where(predicate).ToListAsync();
        }

     

        public async Task<Response<Favorites>> RemoveFavAsync(int memberId, int advertId)
        {
            try
            {
                var favorites = await GetListAsync(f => f.MemberId == memberId && f.AdvertId == advertId);

                if (favorites == null || !favorites.Any())
                {
                    return Response<Favorites>.Fail("Belirtilen üye ve reklama ait favori bulunamadı.");
                }

                foreach (var favorite in favorites)
                {
                    _uow.GetGenericDal<Favorites>().Delete(favorite);
                }

                await _uow.SaveChangeAsync();

                return Response<Favorites>.Success("Favori başarıyla silindi");
            }
            catch (Exception ex)
            {
                return Response<Favorites>.Fail($"Favori silinirken bir hata oluştu: {ex.Message}");
            }
        }
       
        public async Task<Favorites> GetByAdvertIdAndMemberIdAsync(int advertId, int memberId)
        {
           
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.AdvertId == advertId && f.MemberId == memberId);

            return favorite;
        }




    }
}