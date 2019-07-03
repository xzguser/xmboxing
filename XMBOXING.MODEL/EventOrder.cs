using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.MODEL
{
    //赛事订单数据
    public class EventOrder
    {
        //赛事编号
        public int ComID { get; set; }

        //投注订单号
        public int betID { get; set; }

        //投注用户名称
        public string AccountName { get; set; }

        //赛事主题
        public string GameTheme { get; set; }

        //赛事名称
        public string GameName { get; set; }

        //投注的时间
        public DateTime CreateDate { get; set; }

        //游戏的类型
        public string TypeName { get; set; }

        //游戏的名称
        public string MethodName { get; set; }

        //第一个参赛人员
        public string ParticipantOne { get; set; }

        //第二个参赛人员
        public string ParticipantTwo { get; set; }

        //投注了多少积分
        public int Integral { get; set; }

        //投注订单的状态(0:未结算,1:已结算)
        public int betState { get; set; }
    }
}
