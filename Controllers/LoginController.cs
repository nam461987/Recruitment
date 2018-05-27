using _1C7BEC44.Models;
using _1C7BEC44.Service;
using cModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Website.Services;

namespace cotoiday_admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_Admin_UserAuth md)
        {
            var error = String.Empty;
            if (md.UserName != null || md.UserName != "")
            {
                var convertPass = WebsiteExtension.EncryptPassword(md.PasswordHash);
                try
                {
                    var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

                    var objCheckExist = new GCRequest
                    {
                        _a = "fGettbl_Admin_UserAuth", //Action prefix f,p for get data; gc_App is table name
                        _c = new Dictionary<string, object>
                    {
                        {"UserName", md.UserName },
                        {"PasswordHash",convertPass },
                        {"Status",1 }
                    },
                        _f = "Id,TypeId,UserName,GroupTypeId"
                    };
                    var robjCheckExist = service.P(objCheckExist);
                    if (robjCheckExist.TotalRecordCount > 0)
                    {
                        Session["UserID"] = robjCheckExist.Records[0][0].ToString();
                        Session["RoleId"] = robjCheckExist.Records[0][1].ToString();
                        Session["UserName"] = robjCheckExist.Records[0][2].ToString();
                        Session["GroupId"] = robjCheckExist.Records[0][3].ToString();
                        Session.Timeout = 120;
                        return Redirect("/");
                    }
                    else
                    {
                        return Redirect("/Login/Login");
                    }
                }
                catch (Exception ex)
                {
                    return Redirect("/Login/Login");
                }
            }
            else
            {
                return Redirect("/Login/Login");
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return Redirect("/Login/Login");
        }
        [HttpGet]
        public ActionResult ChangePass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePass(tbl_Admin_UserAuth md, string OldPassWord)
        {
            var convertPass = WebsiteExtension.EncryptPassword(md.PasswordHash);
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            var objCheckPass = new GCRequest
            {
                _a = "fGettbl_Admin_UserAuth", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                    {
                        {"UserName", md.UserName },
                        {"PasswordHash", WebsiteExtension.EncryptPassword(OldPassWord)},
                        { "Status",1}
                    },
                _f = "Id"
            };
            var robjCheckPass = service.P(objCheckPass);
            if (robjCheckPass.TotalRecordCount > 0)
            {
                var obj = new GCRequest
                {
                    _a = "Updatetbl_Admin_UserAuth", //Action prefix f,p for get data; gc_App is table name
                    _c = new Dictionary<string, object>
                    {
                        {"Id", md.Id }
                    },
                    _d = new Dictionary<string, object>
                    {
                        {"PasswordHash", convertPass },
                        {"ModifiedDate",DateTime.Now }
                    }
                };
                var robj = service.P(obj);
                if (robj.Result == 1)
                {
                    return Redirect("/Login/Login");
                }
            }
            return View();
        }
    }
}