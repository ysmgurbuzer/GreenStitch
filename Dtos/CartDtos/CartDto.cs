using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.CartDtos
{
    public class CartDtos
    {
        public int Id { get; set; }
        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>(); // Ensure Items is initialized
        public decimal TotalAmount { get; set; }


        public class ShoppingCartItem
        {
            public decimal Price
            {
                get; set;
            }

            public int AdvertId
            {
                get; set;
            }
            public string? ImagePathOne
            {
                get; set;
            }
            public string? AdvertTitle
            {
                get; set;
            }



            public int? CategoryId { get; set; }


            public int ProductId
            {
                get; set;
            }

            public int Quantity
            {
                get; set;
            }


        }


    }
}