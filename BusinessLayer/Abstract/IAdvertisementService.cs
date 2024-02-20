using Dtos;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IAdvertisementService:IGenericService<AdvertisementCreateDto,AdvertisementUpdateDto,AdvertisementListDto,Advertisement>
    {
    }
}
