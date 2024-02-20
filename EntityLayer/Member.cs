using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public  class Member : IdentityUser<int>
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public decimal? WalletAmount { get; set; } =0.0m;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; } = null!;

        public ICollection<MemberRole> MemberRoles { get; set; } = new List<MemberRole>();
        public  ICollection<Advertisement> Advertisement { get; set; } = new List<Advertisement>();
        public ICollection<RecyclingHistory> RecyclingHistories { get; set; } = new List<RecyclingHistory>();
        public ICollection<Favorites> Favorites { get; set; } = new List<Favorites>();
        public ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();
 
    }
}
