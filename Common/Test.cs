using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cotoiday_admin.Common
{
    public class TestAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionParams = filterContext.ActionParameters;

            object md;
            actionParams.TryGetValue("md", out md);
            if (md != null)
            {
                ((_1C7BEC44.Models.tbl_UserAuth)md).Id = 222;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}