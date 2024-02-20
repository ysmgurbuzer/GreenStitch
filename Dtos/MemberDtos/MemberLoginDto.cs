using Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class MemberLoginDto:IDto
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
