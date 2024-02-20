using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public  class Role : IdentityRole<int>
    {
        [Key]
        public int RoleId { get; set; }
        public string? RoleTitle { get; set; }
        public ICollection<MemberRole> MemberRoles { get; set; } = new List<MemberRole>();
    }
}