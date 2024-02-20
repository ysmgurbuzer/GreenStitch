using AutoMapper;
using BusinessLayer.Mapps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer._Helpers
{
    public static class ProfileHelpers
    {
        public static List<Profile> GetProfiles()
        {

            return new List<Profile>
            {
                new AdvertisementProfile(),
                new CategoryProfile(),
                new FabricTypeProfile(),
                new FavoritesProfile(),
                new MemberProfile(),    
                new OrderHistoryProfile(),
                new ProductProfile(),
                new RecyclingHistoryProfile(),
                new CartProfile(),
                new ProductSizeProfile(),

            };




        }



    }
}
