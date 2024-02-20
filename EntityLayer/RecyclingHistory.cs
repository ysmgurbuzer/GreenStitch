using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public  class RecyclingHistory
    {
        [Key]
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int CategoryId {  get; set; } 
        public decimal Quantity { get; set; }
        public DateTime RecycledDate { get; set; }

     
        public string Status { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; } = null!;
        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
    }
}
