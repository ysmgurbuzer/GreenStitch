using Dtos.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class ProductListDto:IDto
    {
        public int ProductId { get; set; }
        public int AdvertId { get; set; }
        public string? ProductName { get; set; }
        public ProductSize Size { get; set; }
        public string? Color { get; set; }
        public int FabricId { get; set; }
        public decimal Price { get; set; }
    }
}
