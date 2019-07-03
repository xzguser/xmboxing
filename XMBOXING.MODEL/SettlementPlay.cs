using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.MODEL
{
    ///结算的玩法的实体类
    public class SettlementPlay
    {
        ///玩法关联ID
        public int MethodID { get; set; }

        ///玩法类型
        public string TypeName { get; set; }

        ///玩法名称
        public string MethodName { get; set; }

        ///玩法ID
        public int PlayID { get; set; }

        ///公司ID
        public int CompanyID { get; set; }

        //是否胜利
        public int IsWin { get; set; }

        //第一个参赛人员
        public string ParticipantOne { get; set; }

        //第二个参赛人员
        public string ParticipantTwo { get; set; }
    }
}
