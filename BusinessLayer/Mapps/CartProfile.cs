using AutoMapper;
using CommonLayer.CustomErrorHandler;
using Dtos;
using Dtos.CartDtos;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Mapps
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartDtos, Advertisement>().ReverseMap();
            CreateMap<CartDtos, Product>().ReverseMap();
            CreateMap<CartDtos.ShoppingCartItem, Advertisement>().ReverseMap();
            CreateMap<CartDtos.ShoppingCartItem, Product>().ReverseMap();

            CreateMap<AdvertisementListDto, CartDtos.ShoppingCartItem>()
            .ForMember(dest => dest.AdvertId, opt => opt.MapFrom(src => src.AdvertId))
            .ForMember(dest => dest.ImagePathOne, opt => opt.MapFrom(src => src.ImagePathOne))
            .ForMember(dest => dest.AdvertTitle, opt => opt.MapFrom(src => src.AdvertTitle))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => 1)); // Set Quantity to 1 for new items

            CreateMap<ProductListDto, CartDtos.ShoppingCartItem>()
                .ForMember(dest => dest.AdvertId, opt => opt.MapFrom(src => src.AdvertId))
                .ForMember(dest => dest.AdvertTitle, opt => opt.MapFrom(src => src.ProductName)) // Adjust this mapping based on your property names
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => 1)); // Set Quantity to 1 for new items

        }
    }
}