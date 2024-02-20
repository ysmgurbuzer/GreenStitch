using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public  class Product
    {
        [Key]
        public int ProductId { get; set; }
        public int AdvertId { get; set; }
        public string? ProductName { get; set; }
        public string? Color { get; set; }
        public int FabricId { get; set; }
        public decimal Price { get; set; }
       

      

        [ForeignKey("AdvertId")]
        public Advertisement Advertisement { get; set; } = null!;
        [ForeignKey("FabricId")]
        public FabricType FabricType { get; set; } = null!;
        public ICollection<ProductSize> Sizes { get; set; } = new List<ProductSize>();
    }

    public class ProductSize
    {
        [Key]
        public int ProductSizeId { get; set; }
        public int ProductId { get; set; }
        public string? Size { get; set; }
        public int Stock { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }

}
    

