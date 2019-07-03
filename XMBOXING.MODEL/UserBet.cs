using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.MODEL
{

    //用户下注表(text)
    public  class UserBet
    {
            //自增ID
            public int ID { get;set; }

            
            
            //下注的用户
            public string AccountName { get; set; }

            //玩法的ID
            public int playID { get; set; }
            
            //赛事ID
            public int ComID { get; set; }
            
            //用户投注的积分
            public int Integral { get; set; }

             //用户赢的积分
            public int WinIntegral { get; set; } = 0;

            //订单的状态
            public int betState { get; set; } = 0;

            //创建时间
            public DateTime CreateDate { get; set; } = DateTime.Now;
            
            //投注了谁(参赛人员)
            public string Contestants { get; set; }

    }
}
