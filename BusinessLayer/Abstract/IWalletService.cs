using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IWalletService
    {
        Task EarnCouponsAsync(int memberId, int recyclingHistoryId);
    }
}
