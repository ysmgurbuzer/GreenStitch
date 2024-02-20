using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public  class FabricType
    {
        [Key]
        public int FabricId { get; set; }
        public string? FabricName { get; set;}
        public string? FabricDescription { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
