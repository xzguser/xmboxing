using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMBOXING.MODEL
{
    public class UserSettlement
    {
        //赛事Id
        public int ComID { get; set; }

        //投注了谁
        public string Contestants { get; set; }

        //玩法的ID
        public int PlayID { get; set; }
    }
}
