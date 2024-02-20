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
    public class FavoritesProfile:Profile
    {
        public FavoritesProfile()
        {
                CreateMap<FavoritesCreateDto,Favorites>().ReverseMap();
                CreateMap<FavoritesListDto, Favorites>().ReverseMap();
                CreateMap<FavoritesUpdateDto, Favorites>().ReverseMap();
        }
    }
}
