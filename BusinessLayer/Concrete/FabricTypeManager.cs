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
    public class FabricTypeManager : GenericManager<FabricTypeCreateDto, FabricTypeUpdateDto, FabricTypeListDto, FabricType>, IFabricTypeService
    {
        public FabricTypeManager(IMapper mapper,
            IValidator<FabricTypeCreateDto> createDto,
            IValidator<FabricTypeUpdateDto> updateDto,
            IValidator<FabricTypeListDto> listDto,
            IUow uow) : base(mapper, createDto, listDto, updateDto, uow)
        {
        }
    }
}
