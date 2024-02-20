using AutoMapper;
using BusinessLayer.Abstract;
using CommonLayer.CustomErrorHandler;
using DataAccessLayer.UnitOfWork;
using Dtos.Abstract;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class GenericManager<CreateDto, UpdateDto, ListDto, T> : IGenericService<CreateDto, UpdateDto, ListDto, T>
    where T : class
    where CreateDto : class, IDto, new()
    where UpdateDto : class, IUpdateDto, new()
    where ListDto : class, IDto, new()
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createDto;
        private readonly IValidator<UpdateDto> _updateDto;
        private readonly IValidator<ListDto> _listDto;
        private readonly IUow _uow;
        public GenericManager(IMapper mapper,
            IValidator<CreateDto> createDto,
            IValidator<ListDto> listDto,
            IValidator<UpdateDto> updateDto,
            IUow uow)
        {
            _mapper = mapper;
            _createDto = createDto;
            _updateDto = updateDto;
            _listDto = listDto;
            _uow = uow;

        }

        public async Task<Response<ListDto>> GetByIdAsync<IDto>(int id)
        {

            var data = await _uow.GetGenericDal<T>().GetByIdAsync(id);

            if (data == null)
            {
                return  Response<ListDto>.Fail("Idye ait veri bulunamadı");
            }
            var dto = _mapper.Map<ListDto>(data);
            return  Response<ListDto>.Success(dto);
        }

        public async Task<Response<List<ListDto>>> GetListAsync()
        {
            var data = await _uow.GetGenericDal<T>().GetListAsync();
            if (data == null)
            {
                return  Response<List<ListDto>>.Fail("Veri Bulunamadı");
            }
            var dto = _mapper.Map<List<ListDto>>(data);
            return  Response<List<ListDto>>.Success(dto);
        }

        public async Task<Response<ListDto>> FindAsync<IDto>(int id)
        {

            var data = await _uow.GetGenericDal<T>().FindAsync(id);

            if (data == null)
            {
                return Response<ListDto>.Fail("Idye ait veri bulunamadı");
            }
            var dto = _mapper.Map<ListDto>(data);
            return Response<ListDto>.Success(dto);
        }


        public async Task<Response<CreateDto>> AddAsync(CreateDto dto)
        {
            var result = _createDto.Validate(dto);
            if (result.IsValid)
            {
                var created= _mapper.Map<T>(dto);
                await _uow.GetGenericDal<T>().AddAsync(created);
                try
                {
                    await _uow.SaveChangeAsync();
                    return Response<CreateDto>.Success(dto);
                }
                catch (Exception ex)
                {
                  
                    return Response<CreateDto>.Fail("Verileriniz kaydedilirken bir hata oluştu.");
                }

            }
            return  Response<CreateDto>.Fail("Verileriniz istenen özelliklerle uyumsuz");
        }

        public async Task<Response<T>> RemoveAsync(int id)
        {
           var data=await _uow.GetGenericDal<T>().GetByIdAsync(id);

            if(data==null)
                return  Response<T>.Fail("Idye ait veri bulunamadı");

            _uow.GetGenericDal<T>().Delete(data);
            await _uow.SaveChangeAsync();
            return  Response<T>.Success("Silme İşlemi Başarılı");
        }


        public async Task<Response<UpdateDto>> UpdateAsync(UpdateDto dto)
        {

            var result = _updateDto.Validate(dto);
            if (result.IsValid)
            {
                var data = await _uow.GetGenericDal<T>().FindAsync(dto.Id);
                if (data == null)
                {
                    return  Response<UpdateDto>.Fail("Idye ait güncellenecek veri bulunamadı");
                }

                var entity=_mapper.Map<T>(dto);
                _uow.GetGenericDal<T>().Update(entity,data);
                await _uow.SaveChangeAsync();
                return  Response<UpdateDto>.Success("Güncelleme İşlemi Başarılı");

            }
            return  Response<UpdateDto>.Fail("Verileriniz istenen özelliklerle uyumsuz");

        }


    }      
}
