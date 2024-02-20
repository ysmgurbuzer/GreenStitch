using APIGreenStitch.Models;
using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using Dtos;
using EntityLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace APIGreenStitch.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly GreenContext _context;
        private readonly IRecyclingHistoryService _recyclingHistoryService;
        private readonly IProductSizeService _size;
        private readonly ICategoryService _categoryService;
     
        public AdvertisementController(IAdvertisementService advertisementService,
            IMapper mapper,
            GreenContext context,
            IProductService productService,
            IRecyclingHistoryService recycling,
            IProductSizeService size, ICategoryService categoryService)
        {
                _advertisementService = advertisementService;
            _mapper = mapper;
            _context = context; 
            _productService = productService;
            _recyclingHistoryService = recycling;   
            _size = size;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAdvert(AdvertProductApiModel dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int memberId = _recyclingHistoryService.GetAuthenticatedMember();
            dto.Advertisement.MemberId = memberId;
            var advertsResponse = await _advertisementService.AddAsync(dto.Advertisement);
            if (advertsResponse == null || advertsResponse.Data == null)
            {
                return BadRequest("Failed to create Advertisement.");
            }
                var advertMapper = _mapper.Map<Advertisement>(advertsResponse.Data);
            if (advertMapper == null)
            {
                return BadRequest("Failed to map Advertisement data.");
            }

            int advertId = advertMapper.AdvertId;
            dto.Product.AdvertId = advertId;
                

            try
            {
              
                var productsResponse = await _productService.AddAsync(dto.Product);
                if (productsResponse == null || productsResponse.Data == null)
                {
                    await _advertisementService.RemoveAsync(advertId);
                    return BadRequest("Failed to create Product or Product data is null.");
                }

                var productMapper = _mapper.Map<Product>(productsResponse.Data);
             
             

               


                return Ok(new { Advertisement = advertMapper, Product = productMapper});
            }
            catch (Exception ex)
            {
                await _advertisementService.RemoveAsync(advertId);
                throw;
            }
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAdvertisement(int id)
        {
          
                var product = await _productService.FindAsync<ProductListDto>(id);

                if (product == null)
                {
                    return NotFound("Product not found.");
                }


                var productResponse = await _productService.RemoveAsync(id);
                int  advertId= product.Data.AdvertId;
                var advertResponse = await _advertisementService.RemoveAsync(advertId);

            if (productResponse != null && advertResponse != null)
            {
                
                return Ok();
            }
            else
            {
                
                return BadRequest("Failed to remove Product or Advertisement.");
            }


        }

      


    }

}

