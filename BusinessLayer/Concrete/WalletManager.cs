using AutoMapper.Execution;
using BusinessLayer.Abstract;
using DataAccessLayer.UnitOfWork;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class WalletManager:IWalletService
    {
        private readonly IUow _uow;
        public WalletManager(IUow uow)
        {
                _uow = uow; 
        }
        public async Task EarnCouponsAsync(int memberId,int recyclingHistoryId)
        {
            var recyclingHistory = await _uow.GetGenericDal<RecyclingHistory>().GetByIdAsync(recyclingHistoryId);

            if (recyclingHistory != null && recyclingHistory.Status == "Approved")
            {
                
                var walletAmount = recyclingHistory.Quantity * 25.0m;

                recyclingHistory.Status = "Coupon Defined";
                var user = await _uow.GetGenericDal<EntityLayer.Member>().GetByIdAsync(memberId);
                if (user != null)
                {
                    user.WalletAmount += walletAmount;
                  
                }
                await _uow.SaveChangeAsync();

            }
        }
       
    }
}
