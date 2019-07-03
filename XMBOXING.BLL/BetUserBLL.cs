using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.DAL;
using XMBOXING.MODEL;

namespace XMBOXING.BLL
{
    public class BetUserBLL
    {
        private BetUserDAL betUserDAL = new BetUserDAL();

        //用户投注订单
        public bool InsertUserRecord(UserBet userBet )
        {
           return betUserDAL.InsertUserBetting(userBet);
        }
    }
}
