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

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-28
    /// 修改时间：
    /// 功能：赛事玩法关联数据访问实现类
    /// </summary>
    public class GameMethodDAL:BaseDAL<GameMethodEntity>,IGameMethodDAL
    {
        public GameMethodDAL() {
            this.ToTable("tbGameMethod");
            this.ToKey("ID");
        }

        public IQueryable<GameMethodDTO> GetGameMethodData() {

            string sql = "  select b.ID AS CompanyID,b.CompanyName,d.ID as MethodID,d.MethodName,d.TypeName,c.GameName,c.GameTheme,c.ID as CompetitionID " +
                "from tbGameMethod  a  " +
                "join tbCompany b on a.CompanyID=b.ID  " +
                "join tbPlayMethod d on a.PlayMethodID=d.ID  " +
                "right join tbCompetition c on a.CompetitionID=c.ID  where c.GameType=0";
           return  QueryList<GameMethodDTO>(sql).AsQueryable();
        }

        /// <summary>
        /// 查询改赛事的玩法
        /// </summary>
        /// <param name="aintComprtitionID">赛事ID</param>
        /// <returns></returns>
        public IQueryable<GameMethodDTO> GetPlayMethodByComprtition(int aintComprtitionID)
        {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@ComprtitionID",aintComprtitionID);
            return QueryList<GameMethodDTO>("P_GameMethod_GetPlayMethodByComprtition",objParam,System.Data.CommandType.StoredProcedure).AsQueryable();
        }


        /// <summary>
        /// 在结算时或得改赛事的玩法
        /// </summary>
        /// <param name="aintComprtitionID">赛事id</param>
        /// <returns></returns>
        public IQueryable<GameMethodDTO> GetGameMethodByResult(int aintComprtitionID) {
            string strSql = "select TypeName, MethodName, a.ID as MethodID, Sum(c.Integral) as TotalIntegral from tbGameMethod a join tbPlayMethod b on a.PlayMethodID = b.ID left join tbBet c on c.MethodID = a.ID where a.CompetitionID = @CompetitionID  and MethodStatus = 0 group by  TypeName,MethodName,a.ID";
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@CompetitionID",aintComprtitionID);
            return QueryList<GameMethodDTO>(strSql,objParam).AsQueryable();
        }

        //结算时获取该赛事的玩法
        public IQueryable<SettlementPlay> GetSettlementPlays(int aintComprtitionID)
        {
            string strSql = "select gm.ID as MethodID,pm.TypeName,pm.MethodName,gm.CompanyID,gm.IsWin,cp.ParticipantOne,cp.ParticipantTwo,pm.ID as PlayID from tbGameMethod gm,tbPlayMethod pm,tbCompetition cp  ";
            strSql += "where gm.PlayMethodID=pm.ID and gm.CompetitionID=cp.ID and gm.CompetitionID=@CompetitionID";
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@CompetitionID", aintComprtitionID);
            return QueryList<SettlementPlay>(strSql, objParam).AsQueryable();
        }


        //查询出该赛事的玩法及参赛人员
        public IQueryable<GameMethodText> GetGamemethodbyText(int aintComprtitionID)
        {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@ComprtitionID", aintComprtitionID);
            System.Diagnostics.Debug.WriteLine("--------------------------------");
            System.Diagnostics.Debug.WriteLine(objParam);
            return QueryList<GameMethodText>("P_GameMethod_test", objParam, System.Data.CommandType.StoredProcedure).AsQueryable();
        }

        //获取当前赛事的所有订单号
        public IQueryable<EventOrder> GetEventOrders(int aintComprtitionID)
        {
            Dictionary<string, object> objParam = new Dictionary<string, object>();
            objParam.Add("@ComprtitionID", aintComprtitionID);
            string strSql = "select ct.ID as ComID,ub.ID as betID,ub.AccountName,ct.GameTheme,ct.GameName,ub.CreateDate,pm.TypeName,pm.MethodName,ct.ParticipantOne,ct.ParticipantTwo,ub.Integral,ub.betState";
            strSql += " from tbUserbet ub , tbCompetition ct ,tbPlayMethod pm where ub.ComID = ct.ID and ub.playID = pm.ID and ct.GameType = 1 and ct.ID = @ComprtitionID";
            return QueryList<EventOrder>(strSql,objParam).AsQueryable();
        }

        //结算所有用户的所有订单
        public bool UpdateUserOrders(List<UserSettlement> userSettlements)
        {
            System.Diagnostics.Debug.WriteLine(userSettlements);
           // int length = userSettlements.Count;
            int count = 0;
            int sum = 0;
            foreach (var item in userSettlements)
            {
                Dictionary<string, object> objParam = new Dictionary<string, object>();
                objParam.Add("@ComID", item.ComID);
                objParam.Add("@playID", item.PlayID);
                objParam.Add("@Contestants", item.Contestants);
                //return QuerySingle<int>("P_User_Betting", objParam, CommandType.StoredProcedure) == 0 ? true : false;
                int i = QuerySingle<int>("P_User_Settlement", objParam, CommandType.StoredProcedure);
                sum += 1;
                System.Diagnostics.Debug.WriteLine(sum);
                count += i;

            }
            if(count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        /// <summary>
        /// 修改赢的玩法
        /// </summary>
        /// <param name="aintMethodID">玩法ID集合</param>
        /// <returns></returns>
        public bool UpdateWinMethod(List<int> aintMethodID) {
            string strSql = "update tbGameMethod Set IsWin=1 where ID in @Ids";
            return Execute(strSql, new { Ids = aintMethodID.ToArray() }) >0?true:false;
        }


        /// <summary>
        /// 删除多条游戏设定记录
        /// </summary>
        /// <param name="aobjGameMethodIDs">游戏设定ID集合</param>
        /// <returns></returns>
        public bool DeleteGameMethodMore(List<int> aobjGameMethodIDs)
        {
            string strSql = "Delete tbGameMethod where PlayMethodID in @aobjGameMethodIDs";
            return Execute(strSql,new { aobjGameMethodIDs =aobjGameMethodIDs})>0?true:false;
        }
    }
}
