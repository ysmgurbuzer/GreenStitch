using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public  class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int GenderId { get; set; }
      
        public ICollection<Advertisement> Advertisement { get; set; } = new List<Advertisement>();
        public ICollection<RecyclingHistory> RecyclingHistory { get; set; } = new List<RecyclingHistory>();
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }
        
      
    }
}

