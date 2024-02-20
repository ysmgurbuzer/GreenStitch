using Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class ProductSizeListDto:IDto
    {
        public int ProductSizeId { get; set; }
        public int ProductId { get; set; }
        public string? Size { get; set; }
        public int Stock { get; set; }

    }
}
