using Dtos.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class ProductUpdateDto : IUpdateDto
    {
        public int Id {  get; set; }
        public int ProductId { get; set; }
        public int AdvertId { get; set; }
        public string? ProductName { get; set; }
        public string? Color { get; set; }
        public int FabricId { get; set; }
        public decimal Price { get; set; }
    }
}
