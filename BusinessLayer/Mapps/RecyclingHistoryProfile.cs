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
    public class RecyclingHistoryProfile:Profile
    {
        public RecyclingHistoryProfile()
        {
            CreateMap<RecyclingHistoryCreateDto, RecyclingHistory>().ReverseMap();
            CreateMap<RecyclingHistoryListDto, RecyclingHistory>().ReverseMap();
        }
    }
}
