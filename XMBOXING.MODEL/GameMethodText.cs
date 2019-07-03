using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.MODEL
{
    public class GameMethodText
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public int CompanyID { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 玩法关联ID
        /// </summary>
        public int MethodID { get; set; }

        /// <summary>
        /// 玩法
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 玩法类型
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 赛事名字
        /// </summary>
        public string GameName { get; set; }

        /// <summary>
        /// 赛事主题
        /// </summary>
        public string GameTheme { get; set; }

        /// <summary>
        /// 赛事ID
        /// </summary>
        public int CompetitionID { get; set; }

        /// <summary>
        /// 玩法总积分
        /// </summary>
        public int Integral { get; set; }

        /// <summary>
        /// 是否胜利
        /// </summary>
        public int IsWin { get; set; }

        //玩法的ID
        public int PlayMethodID { get; set; }

        //第一个参赛人员
        public string ParticipantOne { get; set; }
        //第二个参赛人员
        public string ParticipantTwo { get; set; }

        //当前玩法的总投注条数
        public int SeleNumber { get; set; }

        //投注第一个参赛人员的条数
        public int PlayerOne { get; set; }

        //投注平局的总条数
        public int PlayerFlat { get; set; }

        //投注第二个玩家的总条数
        public int PlayerTwo { get; set; }
    }
}
