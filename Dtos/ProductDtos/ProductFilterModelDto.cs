﻿using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.ProductDtos
{
    public class ProductFilterModelDto
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Color { get; set; }
        public string? ProductName { get; set; }
        public int? CategoryId { get; set; }
        public int? FabricId { get; set; }
        public ProductSize Size { get; set; }
        //gender

    }
}
