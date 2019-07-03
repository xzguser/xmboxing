using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using XMBOXING.BLL;
using XMBOXING.Comm;
using XMBOXING.MODEL;

namespace XMBOXING.Backstage.Controllers
{

    /// <summary>
    /// 作者：邓镇康
    /// 创建时间:2019-5-31
    /// 修改时间
    /// 功能：玩法设定控制类
    /// </summary>
    public class GameMethodController : BaseController
    {

        /// <summary>
        /// 玩法逻辑处理对象
        /// </summary>
        private GameMethodBLL mobjGameMethodBLL = new GameMethodBLL();

        /// <summary>
        /// 公司逻辑处理对象
        /// </summary>
        private CompanyBLL mobjComPanyBLL = new CompanyBLL();

        /// <summary>
        /// 玩法逻辑处理对象
        /// </summary>
        private PlayMethodBLL mobjPlayMethodBLL = new PlayMethodBLL();

        /// <summary>
        /// 赛事设定逻辑处理对象
        /// </summary>
        private CompetitionBLL mobjCompetitionBLL = new CompetitionBLL();


        //下注对象表
        private BetUserBLL betUserBLL = new BetUserBLL();


        /// <summary>
        /// 玩法设定视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获得玩法设定数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetGameMethodSetting() {
            System.Diagnostics.Debug.WriteLine("Ok");
            IQueryable<CompanyEntity> objCompanys = mobjComPanyBLL.GetAll();
            IQueryable<PlayMethodEntity> objPlayMethod = mobjPlayMethodBLL.GetAll();
            IQueryable<GameMethodDTO> objGameMethodDTOs=mobjGameMethodBLL.GetGameMethodData();
            IQueryable<CompetitionEntity> objCompetitions = mobjCompetitionBLL.GetComprtitionByNotStart();
            var objData = new
            {
                Company = objCompanys,
                PlayMethod = ClassifyPlayMethod(objPlayMethod),
                GameMethod = objGameMethodDTOs,
                Competition=objCompetitions
            };
            System.Diagnostics.Debug.WriteLine(objCompetitions);

            return Content(JsonConvert.SerializeObject(objData));  
        }

        /// <summary>
        /// 添加/修改一条游戏玩法记录
        /// </summary>
        /// <param name="aobjGameMethod">游戏玩法实体类</param>
        /// <returns></returns>
        public ActionResult InsertOrUpdateGameMethod(GameMethodEntity aobjGameMethod) {
            bool isSuccess = false;
            if (aobjGameMethod.ID != 0)
            {
                isSuccess = mobjGameMethodBLL.UpdateGameMethod(aobjGameMethod);
            }
            else {
                isSuccess = mobjGameMethodBLL.InsertGameMethod(aobjGameMethod);
            }
          
            return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="aobjPlayMethiod">玩法类型集合</param>
        /// <returns></returns>
        public ActionResult InsertMethod(string astrPlayMethodJson) {
            bool isSuccess = mobjPlayMethodBLL.InserMore(JsonConvert.DeserializeObject<List<PlayMethodEntity>>(astrPlayMethodJson));
            return Content(isSuccess.ToString());
        }


        /// <summary>
        /// 修改玩法
        /// </summary>
        /// <param name="aEditEntity">玩法实体类</param>
        /// <returns></returns>
        public ActionResult UpdatePlayMethod(PlayMethodEntity aEditEntity) {

            bool isSuccess=mobjPlayMethodBLL.UpdatePlayMethod(aEditEntity);
            return Content(isSuccess.ToString());

        }

        /// <summary>
        /// 删除玩法
        /// </summary>
        /// <param name="aintID">编号</param>
        /// <returns></returns>
        public ActionResult DeltePlayMethod(int aintID) {

            bool isSuccess = mobjPlayMethodBLL.DeleteExType(aintID);
            return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 在结算时或得改赛事的玩法
        /// </summary>
        /// <param name="aintComprtitionID">赛事id</param>
        /// <returns></returns>
        public ActionResult GetGameMethodByResult(int aintComprtitionID) {
           return Content(JsonConvert.SerializeObject(mobjGameMethodBLL.GetGameMethodByResult(aintComprtitionID)));
        }

        //结算时获取该赛事的玩法
        public ActionResult GetGameMethodPlays(int aintComprtitionID)
        {
            return Content(JsonConvert.SerializeObject(mobjGameMethodBLL.GetGemeMethodPlay(aintComprtitionID)));
        }

        //测试获取该赛事的玩法
        public ActionResult GetGameMethodTest1(int aintComprtitionID)
        {
            return Content(JsonConvert.SerializeObject(mobjGameMethodBLL.GetGamemethodTexts(aintComprtitionID)));
        }

        //根据赛事获取所有订单
        public ActionResult GetEventOrderTest (int aintComprtitionID)
        {
            return Content(JsonConvert.SerializeObject(mobjGameMethodBLL.GetEventAllOrder(aintComprtitionID)));
        }

        //结算用户的所有订单
        public ActionResult SettlementUserOrders( string userSettlements)
        {
           
            JavaScriptSerializer Serializer = new JavaScriptSerializer();
            System.Diagnostics.Debug.WriteLine(userSettlements);
            // var jsonStr = new JavaScriptSerializer().Serialize(userSettlements);


            List<UserSettlement> users = Serializer.Deserialize<List<UserSettlement>>(userSettlements);
           
            return Content(JsonConvert.SerializeObject(mobjGameMethodBLL.SettlementUserAll(users)));
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="aobjResult">前台传来参数</param>
        /// <returns></returns>
        public ActionResult UpdateWinner(string astrResult,int aintBetID) {

            Dictionary<string, List<GameMethodDTO>> objResult = JsonConvert.DeserializeObject<Dictionary<string, List<GameMethodDTO>>>(astrResult);

            return Content(JsonConvert.SerializeObject(mobjGameMethodBLL.UpdateWinner(objResult,aintBetID)));
        }

        /// <summary>
        /// 删除多条玩法记录
        /// </summary>
        /// <param name="astrGameMethodID"></param>
        /// <returns></returns>
        public ActionResult DeleteGameMethodMore(string astrGameMethodIDs) {
            List<int> objGameMethodIDs = JsonConvert.DeserializeObject<List<int>>(astrGameMethodIDs);
            bool isSuccess = mobjGameMethodBLL.DeleteGameMethodMore(objGameMethodIDs);
            isSuccess = mobjPlayMethodBLL.DeleteMore(objGameMethodIDs);
            return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 得到所有玩法记录
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPlayMethods() {
            IQueryable<PlayMethodEntity> objPlayMethods = mobjPlayMethodBLL.GetAll();
            return Content(JsonConvert.SerializeObject(objPlayMethods));
        }

        /// <summary>
        /// 添加多条赛事玩法记录
        /// </summary>
        /// <param name="aintCompetitionID">赛事ID</param>
        /// <param name="aintCompanyID">公司ID</param>
        /// <param name="astrPlayMethodIDs">玩法集合</param>
        /// <returns></returns>
        public ActionResult InsertGameMethod(int aintCompetitionID,int aintCompanyID,string astrPlayMethodIDs) {
            bool isSuccess= mobjGameMethodBLL.InsertGameMethodMore(aintCompetitionID,aintCompanyID,JsonConvert.DeserializeObject<List<int>>(astrPlayMethodIDs));
            return Content(isSuccess.ToString());
        }

        /// <summary>
        /// 把玩法按类型分类
        /// </summary>
        /// <param name="aobjPlayMethod"></param>
        /// <returns></returns>
        private object ClassifyPlayMethod(IQueryable<PlayMethodEntity> aobjPlayMethod)
        {
            List<string> objMethodType = new List<string>();
            Dictionary<string, List<PlayMethodEntity>> objPlayMethod = new Dictionary<string, List<PlayMethodEntity>>();
            foreach (var item in aobjPlayMethod)
            {
                if (!objMethodType.Contains(item.TypeName)) {
                    objMethodType.Add(item.TypeName);
                    objPlayMethod.Add(item.TypeName,aobjPlayMethod.Where(t=>t.TypeName.Equals(item.TypeName)).ToList());
                }
            }
            return objPlayMethod;
        }


        //显示所有未开始的赛事
        public ActionResult selectComprtition()
        {
            Dictionary<string, object> pairs = new Dictionary<string, object>();
            pairs.Add("StartDate", Request["StartDate"] == null ?DateTime.Now.Date.ToString():Request["StartDate"]);
            pairs.Add("EndDate", Request["EndDate"] == null ? DateTime.Now.Date.ToString() : Request["EndDate"]);


            //if (StartDate == null)
            //{
            //    StartDate = DateTime.Now;

            //}
            //if (EndDate == null)
            //{
            //    EndDate = DateTime.Now;
            //}
            DateTime StartDate = Convert.ToDateTime(pairs["StartDate"].ToString()) ;
            DateTime EndDate = Convert.ToDateTime(pairs["EndDate"].ToString());
            return Content(JsonConvert.SerializeObject(mobjCompetitionBLL.GetComprtitionByNotStart1(StartDate,EndDate)));
        }

        //添加用户投注订单纪录
        public ActionResult InsertUserbetting(UserBet userBet)
        {
            userBet.CreateDate = DateTime.Now;
            bool flag = betUserBLL.InsertUserRecord(userBet);
            return Content(flag.ToString());
        }

    }
}