using Dtos.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class FavoritesCreateDto:IDto
    {
        public int FavoriteId { get; set; }
        public int AdvertId { get; set; }
        public int MemberId { get; set; }
     
    }
}
