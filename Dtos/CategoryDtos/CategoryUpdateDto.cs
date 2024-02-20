using Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class CategoryUpdateDto : IUpdateDto
    {
        public int Id {  get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
