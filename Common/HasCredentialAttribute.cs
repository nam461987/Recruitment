using _1C7BEC44.Models;
using _1C7BEC44.Service;
using cModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Website.Services;

namespace cotoiday_admin.Common
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string Role { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            List<string> privilegeLevels = new List<string>();
            var groupId = HttpContext.Current.Session["GroupId"].ToString();
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "fGettbl_Admin_Group_Permission_View00", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"GroupId",groupId },
                    {"Status", 1}
                },
                _f = String.Join(",", typeof(tbl_Admin_Group_Permission_View00).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj);
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_Admin_Group_Permission_View00>();
                privilegeLevels = result.Select(c => c.PermissionIdCode.ToString()).ToList();
            }
            if (privilegeLevels.Contains(this.Role))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary{{"controller", "Home"}, {"action", "Page_Deny"}});
        }
    }
}