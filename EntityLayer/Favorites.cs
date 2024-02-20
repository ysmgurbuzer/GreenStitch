using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public  class Favorites
    {
        [Key]
        public int FavoriteId { get; set; }
        public int AdvertId { get; set; }
        public int MemberId { get; set; }

        [ForeignKey("AdvertId")]
        public Advertisement Advertisements { get; set;}
        [ForeignKey("MemberId")]
        public Member Member { get; set; } 
    }
}
