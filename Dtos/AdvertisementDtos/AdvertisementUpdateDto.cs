using Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class AdvertisementUpdateDto : IUpdateDto
    {
        public int AdvertId { get; set; }
        public int Id {  get; set; }
        public string? AdvertTitle { get; set; }
        public string? ImagePathOne { get; set; }
        public string? ImagePathTwo { get; set; }
        public string? ImagePathThree { get; set; }
        public int CategoryId { get; set; }
    }
}
