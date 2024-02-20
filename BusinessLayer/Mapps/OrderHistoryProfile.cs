using AutoMapper;
using Dtos;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapps
{
    public class OrderHistoryProfile:Profile
    {
        public OrderHistoryProfile()
        {
                CreateMap<OrderHistoryCreateDto,OrderHistory>().ReverseMap();
                CreateMap<OrderHistoryListDto, OrderHistory>().ReverseMap();
   
        }
    }
}
