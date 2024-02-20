using Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class FabricTypeCreateDto:IDto
    {
        public int FabricId { get; set; }
        public string? FabricName { get; set; }
        public string? FabricDescription { get; set; }
    }
}
