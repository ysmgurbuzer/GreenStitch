using EntityLayer;

namespace APIGreenStitch.Models
{
    public class ProductDetailModel
    {
        public List<ProductDetailModel> ProductDetails { get; set; }
        public Product Product { get; set; }
        public Advertisement Advertisement { get; set; }
        public EntityLayer.Member Member { get; set; }
        public FabricType FabricType { get; set; }
        public ProductSize Size { get; set; }
    }
}
