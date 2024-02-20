using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public  class Gender
    {
        [Key]
        public int GenderId { get; set; }
        public string? GenderType { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public ICollection<Member> Member { get; set; } = new List<Member>();
    }
}
