using AutoMapper;
using BusinessLayer.Abstract;
using CommonLayer.CustomErrorHandler;
using DataAccessLayer.UnitOfWork;
using Dtos;
using Dtos.Abstract;
using EntityLayer;
using FluentValidation;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class OrderHistoryManager:IOrderHistoryService
    {

        private readonly IMapper _mapper;
        private readonly IValidator<OrderHistoryCreateDto> _createDto;
        private readonly IValidator<OrderHistoryListDto>  _listDto;
        private readonly IUow _uow;
        public OrderHistoryManager(IMapper mapper,
            IValidator<OrderHistoryCreateDto> createDto,
            IValidator<OrderHistoryListDto> listDto,
            IUow uow)
        {
            _mapper = mapper;
            _createDto = createDto;
            _listDto = listDto;
            _uow = uow;

        }

        public async Task<Response<OrderHistoryListDto>> GetByIdAsync<IDto>(int id)
        {

            var data = await _uow.GetGenericDal<OrderHistory>().GetByIdAsync(id);

            if (data == null)
            {
                return Response<OrderHistoryListDto>.Fail("Idye ait veri bulunamadı");
            }
            var dto = _mapper.Map<OrderHistoryListDto>(data);
            return Response<OrderHistoryListDto>.Success(dto);
        }

        public async Task<Response<List<OrderHistoryListDto>>> GetListAsync()
        {
            var data = await _uow.GetGenericDal<OrderHistory>().GetListAsync();
            if (data == null)
            {
                return Response<List<OrderHistoryListDto>>.Fail("Veri Bulunamadı");
            }
            var dto = _mapper.Map<List<OrderHistoryListDto>>(data);
            return Response<List<OrderHistoryListDto>>.Success(dto);
        }

        public async Task<Response<int>> AddAsync(OrderHistoryCreateDto dto)
        {
            var result = _createDto.Validate(dto);
            if (result.IsValid)
            {
                var created = _mapper.Map<OrderHistory>(dto);
                await _uow.GetGenericDal<OrderHistory>().AddAsync(created);
                await _uow.SaveChangeAsync();

              
                var savedOrder = await _uow.GetGenericDal<OrderHistory>().FindAsync(created.Id);

                if (savedOrder != null)
                {
                    return Response<int>.Success(savedOrder.Id);
                }
                else
                {
            
                    return Response<int>.Fail("Failed to retrieve the saved order from the database.");
                }
            }

            return Response<int>.Fail("Verileriniz istenen özelliklerle uyumsuz");
        }



        public async Task UpdateOrderStatus(int Id)
        {

            var order = await _uow.GetGenericDal<OrderHistory>().GetByIdAsync(Id);

            if (order != null && order.OrderStatus == "Your Order is Preparing")
            {

                order.OrderStatus = "Your Order Has Been Shipped";
                  await _uow.SaveChangeAsync();

                BackgroundJob.Schedule(() => UpdateOrderStatusToDistribution(order.Id), TimeSpan.FromMinutes(1));
            }


        }
        public async Task UpdateOrderStatusToDistribution(int Id)
        {
            var order = await _uow.GetGenericDal<OrderHistory>().GetByIdAsync(Id);

            if (order != null && order.OrderStatus == "Your Order Has Been Shipped")
            {
                order.OrderStatus = "Cargo is in Delivery";
                await _uow.SaveChangeAsync();
                BackgroundJob.Schedule(() => UpdateOrderStatusToDelivered(order.Id), TimeSpan.FromMinutes(1));
            }
        }

        public async Task UpdateOrderStatusToDelivered(int Id)
        {
            var order = await _uow.GetGenericDal<OrderHistory>().GetByIdAsync(Id);

            if (order != null && order.OrderStatus == "Cargo is in Delivery")
            {
                order.OrderStatus = "Your Order Has Been Delivered";
                await _uow.SaveChangeAsync();
               
            }
        }
    }
}
