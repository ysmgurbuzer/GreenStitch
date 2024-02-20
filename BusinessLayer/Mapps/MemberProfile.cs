using AutoMapper;
using CommonLayer.CustomErrorHandler;
using Dtos;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapps
{
    public class MemberProfile:Profile
    {
        public MemberProfile()
        {
                CreateMap<MemberCreateDto,Member>().ReverseMap();
                CreateMap<MemberListDto, Member>().ReverseMap();
                CreateMap<MemberUpdateDto, Member>().ReverseMap();
            CreateMap<MemberLoginDto, Member>().ReverseMap();
            CreateMap<MemberListDto, List<Member>>().ReverseMap();
            CreateMap<Response<MemberListDto>, Member>().ReverseMap();
        }
    }
}
