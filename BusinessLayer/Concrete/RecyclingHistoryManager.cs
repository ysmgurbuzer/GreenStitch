using AutoMapper;
using BusinessLayer.Abstract;
using CommonLayer.CustomErrorHandler;
using DataAccessLayer.Context;
using DataAccessLayer.UnitOfWork;
using Dtos;
using EntityLayer;
using FluentValidation;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class RecyclingHistoryManager:IRecyclingHistoryService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<RecyclingHistoryCreateDto> _createDto;
        private readonly IValidator<RecyclingHistoryListDto> _listDto;
        private readonly IUow _uow;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWalletService _walletService;
        private readonly GreenContext _context;
        public RecyclingHistoryManager(IMapper mapper,
            IValidator<RecyclingHistoryCreateDto> createDto,
            IValidator<RecyclingHistoryListDto> listDto,
            IUow uow,
            IHttpContextAccessor httpContextAccessor,
            IWalletService walletService,
            GreenContext context)
        {
            _mapper = mapper;
            _createDto = createDto;
            _listDto = listDto;
            _uow = uow;
            _httpContextAccessor = httpContextAccessor;
            _walletService = walletService;
            _context = context;
        }

        public async Task<Response<RecyclingHistoryListDto>> GetByIdAsync<IDto>(int id)
        {

            var data = await _uow.GetGenericDal<RecyclingHistory>().GetByIdAsync(id);

            if (data == null)
            {
                return Response<RecyclingHistoryListDto>.Fail("Idye ait veri bulunamadı");
            }
            var dto = _mapper.Map<RecyclingHistoryListDto>(data);
            return Response<RecyclingHistoryListDto>.Success(dto);
        }





        public async Task<Response<List<RecyclingHistoryListDto>>> GetListAsync()
        {
            var data = await _uow.GetGenericDal<RecyclingHistory>().GetListAsync();
            if (data == null)
            {
                return Response<List<RecyclingHistoryListDto>>.Fail("Veri Bulunamadı");
            }
            var dto = _mapper.Map<List<RecyclingHistoryListDto>>(data);
            return Response<List<RecyclingHistoryListDto>>.Success(dto);
        }






        public async Task<Response<RecyclingHistoryCreateDto>> AddAsync(RecyclingHistoryCreateDto dto)
        {

            var memberId = GetAuthenticatedMember();
         
            var recyc = new RecyclingHistory
            {
                MemberId= memberId, 
                RecycledDate = DateTime.UtcNow,
                CategoryId = dto.CategoryId,    
                Quantity = dto.Quantity,
                Status = "Waiting for Approval",
            };
            var shippingCode = GenerateRandomShippingCode();
          

          
            await _uow.GetGenericDal<RecyclingHistory>().AddAsync(recyc);
            await _uow.SaveChangeAsync();

           var Job1= BackgroundJob.Schedule(() => UpdateRecyclingStatus(recyc.Id), TimeSpan.FromMinutes(1));

            BackgroundJob.ContinueWith(Job1, () => EarnCouponsInBackground(memberId, recyc.Id));

            var createdDto = _mapper.Map<RecyclingHistoryCreateDto>(recyc);

            return Response<RecyclingHistoryCreateDto>.Success(createdDto);
        }

        public async Task<Response<List<RecyclingHistoryListDto>>> ListByMemberIdAsync(int memberId)
        {
            var recycData = await GetListAsync(f => f.MemberId == memberId);

            if (recycData == null)
            {
                return Response<List<RecyclingHistoryListDto>>.Fail("Favori bulunamadı");
            }

            var recycDto = _mapper.Map<List<RecyclingHistoryListDto>>(recycData);
            return Response<List<RecyclingHistoryListDto>>.Success(recycDto);
        }

        public async Task<List<RecyclingHistory>> GetListAsync(Expression<Func<RecyclingHistory, bool>> predicate)
        {
            return await _context.Set<RecyclingHistory>().Where(predicate).ToListAsync();
        }



        public async Task EarnCouponsInBackground(int memberId, int recyclingHistoryId)
        {
           
            await Task.Run(async () =>
            {
                await _walletService.EarnCouponsAsync(memberId, recyclingHistoryId);
            });
        }
        public int GetAuthenticatedMember()
        {
            var nameIdentifierClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (nameIdentifierClaim != null && int.TryParse(nameIdentifierClaim.Value, out int memberId))
            {
                return memberId;
            }

            return -1;
        }


        private string GenerateRandomShippingCode()
        {
            return "RST-" + Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }



        public async Task UpdateRecyclingStatus(int recyclingHistoryId)
        {
            
            var recyclingHistory = await _uow.GetGenericDal<RecyclingHistory>().GetByIdAsync(recyclingHistoryId);

            if (recyclingHistory != null && recyclingHistory.Status == "Waiting for Approval")
            {
               
                recyclingHistory.Status = "Approved";
                await _uow.SaveChangeAsync();

               
            }


        }

    

    }
}
