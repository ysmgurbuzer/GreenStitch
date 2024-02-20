using APIGreenStitch.Models;
using AutoMapper;
using AutoMapper.Execution;
using BusinessLayer.Abstract;
using DataAccessLayer.Context;
using Dtos;
using EntityLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Dtos.ProductDtos;

namespace APIGreenStitch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IAdvertisementService _advertService;
        private readonly IMemberService _memberService;
        private readonly IMapper _mapper;
        private readonly GreenContext _context;
        private readonly IFabricTypeService _fabricTypeService;
        private readonly IProductSizeService _productSizeService;   
        public ProductController(IProductService productService,
            IMapper mapper,
            IAdvertisementService advertisementService,
            IMemberService memberService,
            GreenContext context,
            IFabricTypeService fabricTypeService,
            IProductSizeService productSizeService)
        {

            _productService = productService;
            _mapper = mapper;
            _memberService = memberService;
            _advertService = advertisementService;
            _context = context;
            _fabricTypeService = fabricTypeService; 
            _productSizeService = productSizeService;
        }
        [HttpGet]
        public async Task<IActionResult> ListProduct()
        {
            var productsResponse = await _productService.GetListAsync();
            var memberResponse = await _memberService.GetListAsync();
            var advertisementResponse = await _advertService.GetListAsync();

            if (productsResponse == null || memberResponse == null || advertisementResponse == null)
            {
                return BadRequest();
            }

            var productMapper = _mapper.Map<List<Product>>(productsResponse.Data);
            var memberMapper = _mapper.Map<List<EntityLayer.Member>>(memberResponse.Data);
            var advertisementMapper = _mapper.Map<List<Advertisement>>(advertisementResponse.Data);

        
            foreach (var advertisement in advertisementMapper)
            {
                var member = memberMapper.FirstOrDefault(m => m.Id == advertisement.MemberId);
                if (member != null)
                {
                    advertisement.Member = new EntityLayer.Member
                    {
                        Id = member.Id,
                        Name = member.Name,
                        Surname = member.Surname,

                    };
                }
            }

            var responseData = productMapper.Select(product => new ProductApiModel
            {
                Products = new List<Product> { product },
                Advertisements = advertisementMapper
                    .Where(a => a.AdvertId == product.AdvertId)
                    .ToList()
            }).ToList();

            return Ok(responseData);
        }


        [HttpGet("{advertId}")]
        public async Task<IActionResult> ListProductByProductId(int advertId)
        {
            var advertisementResponse = await _advertService.GetByIdAsync<AdvertisementListDto>(advertId);
            var productResponse = await _productService.GetByIdAsync<ProductListDto>(advertisementResponse.Data.AdvertId);
            var memberResponse = await _memberService.GetByIdAsync<MemberListDto>(advertisementResponse.Data.MemberId);
            var fabricResponse = await _fabricTypeService.GetByIdAsync<FabricTypeListDto>(productResponse.Data.FabricId);
            var productSizeResponse = await _productSizeService.GetByIdAsync<ProductSizeListDto>(productResponse.Data.ProductId);

            var productMapper = _mapper.Map<Product>(productResponse.Data);
            var advertisementMapper = _mapper.Map<Advertisement>(advertisementResponse.Data);
            var memberMapper = _mapper.Map<EntityLayer.Member>(memberResponse.Data);
            var fabricMapper = _mapper.Map<FabricType>(fabricResponse.Data);
            var sizeMapper = _mapper.Map<ProductSize>(productSizeResponse.Data);

            var responseData = new ProductDetailModel
            {
                ProductDetails= new List<ProductDetailModel> {
                    new ProductDetailModel {

                Product = productMapper,
                Advertisement = advertisementMapper,
                FabricType = fabricMapper,
                Member = memberMapper,
                Size = sizeMapper
                
                }
               }
            };

            return Ok(responseData);
        }





        [HttpPut]
        public async Task<IActionResult> UpdateProduct(AdvertProductApiModel dto)
        {
           

            if (ModelState.IsValid)
            { 
                
                
                var productResponse = await _productService.UpdateAsync(dto.ProductUpt);
                var productMapper = _mapper.Map<Product>(productResponse.Data);
              
                var advertisement = await _advertService.FindAsync<AdvertisementUpdateDto>(productResponse.Data.AdvertId);
                var ConvertAdvert = _mapper.Map<AdvertisementUpdateDto>(advertisement);
                var advertisementResponse = await _advertService.UpdateAsync(ConvertAdvert);
                var AdvertMapper = _mapper.Map<Product>(productResponse.Data);

                if (productResponse != null || advertisementResponse!=null)
                {

                    return Ok("Success: Product and Advertisement updated successfully.");
                }
                else return BadRequest();

            }
            else return BadRequest();

           
        }

   

    }

 }


    

 


