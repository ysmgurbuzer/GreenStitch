using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ITokenBlackListService
    {
        void AddToBlacklist(string token);
        bool IsTokenBlacklisted(string token);
    }
}
