using _1C7BEC44.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cotoiday_admin.Controllers
{
    public class BaseController : Controller
    {
        protected S service { get; set; }
        public BaseController() : base()
        {
            service = new S(System.Configuration.ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true);
        }
       
    }
}