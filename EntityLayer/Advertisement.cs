using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public  class Advertisement
    {
        [Key]
        public int AdvertId { get; set; }
        public string? AdvertTitle { get; set; }
        public int MemberId { get; set; }
        public DateTime OrderDate { get; set; }
        public string? ImagePathOne { get; set; }
        public string? ImagePathTwo { get; set; }
        public string? ImagePathThree { get; set; }
        public int CategoryId { get; set; }

      
        public Member Member { get; set; } = null!;
        public  Product Product { get; set; } = null!;
        public  ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();
        public  Category Category { get; set; } = null!;
        public  ICollection<Favorites> Favorites { get; set; } = new List<Favorites>();

    }
}
