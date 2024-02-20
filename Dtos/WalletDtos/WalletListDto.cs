using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletDtos
{
    public class WalletListDto
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public decimal Amount { get; set; }
    }
}
