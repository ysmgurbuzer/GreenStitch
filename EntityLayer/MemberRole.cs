using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class MemberRole
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int MemberId { get; set; }


        public Member Member { get; set; } 
       
        public Role Role { get; set; }
     

    }
}
