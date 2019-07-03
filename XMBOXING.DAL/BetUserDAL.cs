using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.IDAL;
using XMBOXING.MODEL;

namespace XMBOXING.DAL
{
    public class BetUserDAL : BaseDAL<UserBet>, IBetUserDAL
    {
        public BetUserDAL()
        {
            this.ToTable("tbUserbet");
            this.ToKey("ID");
        }

        public bool InsertUserBetting(UserBet user)
        {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            //@AccountName varchar(50),
            //@playID int,
            //@ComID int,
            //@Integral int,
            //@WinIntegral int,
            //@betState int,
            //@CreateDate datetime,
            //@Contestants varchar(50)
            objParam.Add("@AccountName",user.AccountName);
            objParam.Add("@playID", user.playID);
            objParam.Add("@ComID", user.ComID);
            objParam.Add("@Integral", user.Integral);
            objParam.Add("@WinIntegral", user.WinIntegral);
            objParam.Add("@betState", user.betState);
            objParam.Add("@CreateDate", user.CreateDate);
            objParam.Add("@Contestants", user.Contestants);
            // objParam.Add("@ComprtitionID", aintComprtitionID);
            return QuerySingle<int>("P_User_Betting", objParam, CommandType.StoredProcedure) == 0 ? true : false;
            //return QueryList("P_User_Betting", objParam, System.Data.CommandType.StoredProcedure).AsQueryable()=0?true:false;
           // return Insert(user);
        }
    }
}
