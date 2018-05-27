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
using Website.Services;

namespace cotoiday_admin.Controllers
{
    public class DanhMucController : Controller
    {
        // GET: DanhMuc
        public ActionResult Job()
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "fGettbl_Job", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Status", 1},
                    {"NganhId", string.Format("$x is Null")}
                },
                _f = String.Join(",", typeof(tbl_Job).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_Job>();
                result = result.OrderBy(x => x.Name).ToList();
                return View(result);
            }
            return View();
        }
        public JsonResult AddNganh(tbl_Job md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Inserttbl_Job", //Action prefix f,p for get data; gc_App is table name
                _d = new Dictionary<string, object>
                {
                    {"Name", md.Name},
                    {"Code", md.Code}
                }
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            return Json(robj.Result);
        }
        public JsonResult UpdateNganh(tbl_Job md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Updatetbl_Job", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id", md.Id}
                },
                _d = new Dictionary<string, object>
                {
                    {"Name", md.Name},
                    {"Code", md.Code}
                }
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            return Json(robj.Result);
        }
        public JsonResult DeleteNganh(tbl_Job md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            
            var obj = new GCRequest
            {
                _a = "Updatetbl_Job", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id", md.Id}
                },
                _d = new Dictionary<string, object>
                {
                    {"Status", 0}
                }
            };
            var robj = service.P(obj);
            return Json(robj.Result);


        }
        public ActionResult Job_Sub()
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            //Get Job
            var obj = new GCRequest
            {
                _a = "fGettbl_Job", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Status", 1},
                    {"NganhId", string.Format("$x is Null")}
                },
                _f = String.Join(",", typeof(tbl_Job).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_Job>();
                result = result.OrderBy(x => x.Name).ToList();
                ViewBag.Job = result;
            }
            //----------------

            //Get Job-sub
            var objViec = new GCRequest
            {
                _a = "fGettbl_Job_View01", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Status", 1},
                    {"NganhIdStatus", 1},
                    {"NganhId", string.Format("$x is NOT Null")}
                },
                _f = String.Join(",", typeof(tbl_Job_View01).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjViec = service.P(objViec); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            if (robjViec.Result.Equals(1) && robjViec.Records.Any())
            {
                var result = robjViec.Records.ConvertToList<tbl_Job_View01>();
                result = result.OrderBy(x => x.Name).ToList();
                return View(result);
            }
            //--------------

            return View();
        }
        public JsonResult AddCongViec(tbl_Job md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Inserttbl_Job", //Action prefix f,p for get data; gc_App is table name
                _d = new Dictionary<string, object>
                {
                    {"NganhId", md.NganhId},
                    {"Name", md.Name},
                    {"Code", md.Code}
                }
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            return Json(robj.Result);
        }
        public JsonResult DeleteJob_Sub(tbl_Job md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            var obj = new GCRequest
            {
                _a = "Updatetbl_Job", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id", md.Id}
                },
                _d = new Dictionary<string, object>
                {
                    {"Status", 0}
                }
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            return Json(robj.Result);
        }
        public JsonResult UpdateJob_Sub(tbl_Job md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Updatetbl_Job", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id", md.Id}
                },
                _d = new Dictionary<string, object>
                {
                    {"Name", md.Name},
                    {"Code", md.Code}
                }
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            return Json(robj.Result);
        }
        public ActionResult Position()
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "fGettbl_Position", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Status", 1}
                },
                _f = String.Join(",", typeof(tbl_Position).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_Position>();
                result = result.OrderBy(x => x.Name).ToList();
                return View(result);
            }
            return View();
        }
        public JsonResult AddPosition(tbl_Position md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Inserttbl_Position", //Action prefix f,p for get data; gc_App is table name
                _d = new Dictionary<string, object>
                {
                    {"Name", md.Name},
                    {"Code", md.Code}
                }
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            return Json(robj.Result);
        }
        public JsonResult UpdatePosition(tbl_Position md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Updatetbl_Position", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id", md.Id}
                },
                _d = new Dictionary<string, object>
                {
                    {"Name", md.Name},
                    {"Code", md.Code}
                }
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            return Json(robj.Result);
        }
        public JsonResult DeletePosition(tbl_Position md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            var obj = new GCRequest
            {
                _a = "Updatetbl_Position", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id", md.Id}
                },
                _d = new Dictionary<string, object>
                {
                    {"Status", 0}
                }
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            return Json(robj.Result);
        }
    }
}