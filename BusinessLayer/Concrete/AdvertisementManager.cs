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
    public class AdvertisementManager:GenericManager<AdvertisementCreateDto,AdvertisementUpdateDto,AdvertisementListDto,Advertisement>,IAdvertisementService
    {
        public AdvertisementManager(IMapper mapper,
            IValidator<AdvertisementCreateDto> createDto,
            IValidator<AdvertisementListDto> listDto,
            IValidator<AdvertisementUpdateDto> updateDto, 
            IUow uow) : base(mapper, createDto, listDto, updateDto, uow)
        {
        }

    }
}
