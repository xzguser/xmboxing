using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMBOXING.MODEL;

namespace XMBOXING.IDAL
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-28
    /// 修改时间：
    /// 功能：赛事玩法关联信息数据访问接口
    /// </summary>
    public interface IGameMethodDAL:IBaseDAL<GameMethodEntity>
    {

        /// <summary>
        /// 得到赛事设定数据
        /// </summary>
        /// <returns></returns>
        IQueryable<GameMethodDTO> GetGameMethodData();

        /// <summary>
        /// 查询改赛事的玩法
        /// </summary>
        /// <param name="aintComprtitionID">赛事ID</param>
        /// <returns></returns>
        IQueryable<GameMethodDTO> GetPlayMethodByComprtition(int aintComprtitionID);


        /// <summary>
        /// 在结算时或得改赛事的玩法
        /// </summary>
        /// <param name="aintComprtitionID">赛事ID</param>
        /// <returns></returns>
        IQueryable<GameMethodDTO> GetGameMethodByResult(int aintComprtitionID);

        //结算时获取赛事玩法
        IQueryable<SettlementPlay> GetSettlementPlays(int aintComprtitionID);


        //测试得到赛事的玩法及参赛人员
        IQueryable<GameMethodText> GetGamemethodbyText(int aintComprtitionID);


        //（后台管理）根据赛事编号得到所有的订单
        IQueryable<EventOrder> GetEventOrders(int aintComprtitionID);

        //赛事结束，结算用户的所有订单
        bool UpdateUserOrders(List<UserSettlement> userSettlements);


        /// <summary>
        /// 修改赢的玩法
        /// </summary>
        /// <param name="aintMethodID">玩法ID集合</param>
        /// <returns></returns>
        bool UpdateWinMethod(List<int> aintMethodID);


        /// <summary>
        /// 删除多条游戏设定记录
        /// </summary>
        /// <param name="aobjGameMethodIDs">游戏设定ID集合</param>
        /// <returns></returns>
        bool DeleteGameMethodMore(List<int> aobjGameMethodIDs);
    }
}
