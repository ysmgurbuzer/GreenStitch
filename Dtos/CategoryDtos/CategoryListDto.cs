using Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class CategoryListDto:IDto
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int GenderId { get; set; }
    }
}
