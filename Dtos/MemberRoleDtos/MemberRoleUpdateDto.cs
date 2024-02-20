using Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos
{
    public class MemberRoleUpdateDto : IUpdateDto
    {
        public int Id {  get; set; }
        public int RoleId { get; set; }
        public int MemberId { get; set; }
    }
}
