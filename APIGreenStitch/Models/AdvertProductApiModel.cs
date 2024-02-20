using Dtos;
using EntityLayer;

namespace APIGreenStitch.Models
{
    public class AdvertProductApiModel
    {
        public AdvertisementCreateDto? Advertisement { get; set; }
        public ProductCreateDto? Product { get; set; }

        public AdvertisementUpdateDto? AdvertisementUpt { get; set; }
        public ProductUpdateDto? ProductUpt { get; set; }

       public ProductSizeCreateDto? Sizedto { get; set; }

       

    }
}
