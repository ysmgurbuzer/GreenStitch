using Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class RecyclingHistoryCreateDto:IDto
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int CategoryId { get; set; }//Gönderilecek ürünün hangi kategoride olduğunu görmek için
        public decimal Quantity { get; set; }
        public DateTime RecycledDate { get; set; }
    }
}
