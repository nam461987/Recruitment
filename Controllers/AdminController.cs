using _1C7BEC44.Models;
using _1C7BEC44.Service;
using cModel;
using cotoiday_admin.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Services;

namespace cotoiday_admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [HttpGet]
        [HasCredential(Role = "Staff_Account_Creat")]
        public ActionResult Create_Account()
        {
            ViewBag.result = 2;
            return View();
        }
        [HttpPost]
        [HasCredential(Role = "Staff_Account_Creat")]
        public ActionResult CreateAccount(tbl_UserAuth md)
        {
            var convertPass = WebsiteExtension.EncryptPassword(md.PasswordHash);
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Inserttbl_Admin_UserAuth", //Action prefix f,p for get data; gc_App is table name
                _d = new Dictionary<string, object>
                {
                    {"GroupTypeId", md.GroupTypeId},
                    {"UserName", md.UserName.ToLower()},
                    {"PasswordHash", convertPass},
                    {"BirthDate", DateTime.Now},
                    {"StateId",md.StateId },//ID cua nguoi tao tai khoan cho nhan vien
                    {"CreatedDate",DateTime.Now }
                }
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            ViewBag.result = robj.Result;
            ViewBag.type = md.TypeId;
            return View(robj.Records[0][0]);
        }
        public ActionResult SetRole()
        {
            return View();
        }
        public JsonResult CheckUser(tbl_Admin_UserAuth md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "fGettbl_Admin_UserAuth", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"UserName", md.UserName}
                },
                _f = "Id"
            };
            var robj = service.P(obj);
            if (robj.TotalRecordCount > 0)
            {
                return Json(1);
            }
            return Json(0);
        }
    }
}