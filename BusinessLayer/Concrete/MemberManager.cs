using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using Dtos;
using EntityLayer;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MemberManager : GenericManager<MemberCreateDto, MemberUpdateDto, MemberListDto,  Member>, IMemberService
    {
        public MemberManager(IMapper mapper,
            IValidator<MemberCreateDto> createDto,
            IValidator<MemberListDto> listDto,
            IValidator<MemberUpdateDto> updateDto,
            IUow uow) : base(mapper, createDto, listDto, updateDto, uow)
        {
        }
    }
}