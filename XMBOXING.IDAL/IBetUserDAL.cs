using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.MODEL;

namespace XMBOXING.IDAL
{
    public interface IBetUserDAL : IBaseDAL<UserBet>
    {
        //添加用户投注订单
        bool InsertUserBetting(UserBet user);
    }
}
