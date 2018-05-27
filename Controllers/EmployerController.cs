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
    public class EmployerController : BaseController
    {
        // GET: Employer
        public ActionResult Employer_Detail()
        {
            return View();
        }
        public ActionResult _InforTabPartial(int id)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            //Get Province List
            var obj = new GCRequest
            {
                _a = "fGettbl_Provinces", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    { "InUse",1}
                },
                _f = String.Join(",", typeof(tbl_Provinces).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj);
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_Provinces>();
                ViewBag.Province = result;
            }
            //------------------------------------------------------------

            // Get Infor User
            var objUser = new GCRequest
            {
                _a = "fGettbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    { "Status",1},
                    { "Id",id}
                },
                _f = String.Join(",", typeof(tbl_UserAuth).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjUser = service.P(objUser);
            if (robjUser.Result.Equals(1) && robjUser.Records.Any())
            {
                var result = robjUser.Records.ConvertToList<tbl_UserAuth>();
                ViewBag.User = result;
            }
            //-----------------------------------------------------------
            return PartialView();
        }
        public JsonResult UpdateInforEmployer(tbl_UserAuth md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Updatetbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                    {
                        {"Id", md.Id}
                    },
                _d = new Dictionary<string, object>
                    {
                        { "DisplayName",md.DisplayName},
                        { "BirthDate",md.BirthDate},
                        { "CityId",md.CityId},
                        { "Address",md.Address},
                        { "Email",md.Email},
                        { "PhoneNumber",md.PhoneNumber},
                        { "Describe",md.Describe},
                        { "ModifiedDate",DateTime.Now}
                    }
            };
            var robj = service.P(obj);
            return Json(robj.Result);
        }
        [HttpGet]
        public ActionResult CreateNewJob(int? id)
        {
            if (!id.HasValue)
            {
                return Redirect("/");
            }

            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            // Get vi tri
            var objPosition = new GCRequest
            {
                _a = "fGettbl_Position", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Status", 1}
                },
                _f = String.Join(",", typeof(tbl_Position).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjPosition = service.P(objPosition); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            if (robjPosition.Result.Equals(1) && robjPosition.Records.Any())
            {
                var result = robjPosition.Records.ConvertToList<tbl_Position>();
                result = result.OrderBy(x => x.Name).ToList();
                ViewBag.Position = result;
            }

            //Get Job
            var objJob = new GCRequest
            {
                _a = "fGettbl_Job", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Status", 1},
                    {"NganhId", string.Format("$x is Null")}
                },
                _f = String.Join(",", typeof(tbl_Job).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjJob = service.P(objJob); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            if (robjJob.Result.Equals(1) && robjJob.Records.Any())
            {
                var result = robjJob.Records.ConvertToList<tbl_Job>();
                result = result.OrderBy(x => x.Name).ToList();
                ViewBag.Job = result;
            }

            //Get Province List
            var objProvince = new GCRequest
            {
                _a = "fGettbl_Provinces", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    { "InUse",1}
                },
                _f = String.Join(",", typeof(tbl_Provinces).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjProvince = service.P(objProvince);
            if (robjProvince.Result.Equals(1) && robjProvince.Records.Any())
            {
                var result = robjProvince.Records.ConvertToList<tbl_Provinces>();
                ViewBag.Province = result;
            }

            return View(2);
        }
        [HttpPost]
        public ActionResult CreateNewJob(tbl_Recruitment_Post md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Inserttbl_Recruitment_Post", //Action prefix f,p for get data; gc_App is table name
                _d = new Dictionary<string, object>
                    {
                        {"UserId", md.UserId},
                        { "TypeId",md.TypeId},
                        { "TypeIdName",md.TypeIdName},
                        { "Title",md.Title},
                        { "PositionId",md.PositionId},
                        { "Job",","+md.Job+","},
                        { "JobName",md.JobName},
                        { "Location",","+md.Location+","},
                        { "LocationName",md.LocationName},
                        { "Wage",md.Wage},
                        { "TrialTime",md.TrialTime},
                        { "Num",md.Num},
                        { "Experience",md.Experience},
                        { "Diploma",md.Diploma},
                        { "Gender",md.Gender},
                        { "Age",md.Age},
                        { "Describe",md.Describe},
                        { "Interest",md.Interest},
                        { "Other",md.Other},
                        { "Folder",md.Folder},
                        { "EndDate",md.EndDate},
                        { "StaffId",md.StaffId},
                        { "CreateDate",DateTime.Now}
                    }
            };
            var robj = service.P(obj);
            return View(robj.Result);
        }
        public ActionResult _ProjectTabPartial(int id)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            // Get vi tri
            var obj = new GCRequest
            {
                _a = "fGettbl_Recruitment_Post", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"UserId",id },
                    {"Status", 1}
                },
                _f = String.Join(",", typeof(tbl_Recruitment_Post).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_Recruitment_Post>();
                result = result.OrderByDescending(x => x.CreateDate).ToList();
                ViewBag.Post = result;
            }
            return PartialView();
        }
        [HttpGet]
        public ActionResult JobPost_Edit(int id)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            // Get vi tri
            var objPosition = new GCRequest
            {
                _a = "fGettbl_Position", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Status", 1}
                },
                _f = String.Join(",", typeof(tbl_Position).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjPosition = service.P(objPosition); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            if (robjPosition.Result.Equals(1) && robjPosition.Records.Any())
            {
                var result = robjPosition.Records.ConvertToList<tbl_Position>();
                result = result.OrderBy(x => x.Name).ToList();
                ViewBag.Position = result;
            }

            //Get Job
            var objJob = new GCRequest
            {
                _a = "fGettbl_Job", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Status", 1},
                    {"NganhId", string.Format("$x is Null")}
                },
                _f = String.Join(",", typeof(tbl_Job).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjJob = service.P(objJob); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            if (robjJob.Result.Equals(1) && robjJob.Records.Any())
            {
                var result = robjJob.Records.ConvertToList<tbl_Job>();
                result = result.OrderBy(x => x.Name).ToList();
                ViewBag.Job = result;
            }

            //Get Province List
            var objProvince = new GCRequest
            {
                _a = "fGettbl_Provinces", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    { "InUse",1}
                },
                _f = String.Join(",", typeof(tbl_Provinces).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjProvince = service.P(objProvince);
            if (robjProvince.Result.Equals(1) && robjProvince.Records.Any())
            {
                var result = robjProvince.Records.ConvertToList<tbl_Provinces>();
                ViewBag.Province = result;
            }

            // Get vi tri
            var obj = new GCRequest
            {
                _a = "fGettbl_Recruitment_Post", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id",id },
                    {"Status", 1}
                },
                _f = String.Join(",", typeof(tbl_Recruitment_Post).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_Recruitment_Post>();
                result = result.OrderByDescending(x => x.CreateDate).ToList();
                return View(result);
            }
            return View();
        }
        [HttpPost]
        public ActionResult JobPost_Edit(tbl_Recruitment_Post md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Updatetbl_Recruitment_Post", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id",md.Id }
                },
                _d = new Dictionary<string, object>
                {
                    { "TypeId",md.TypeId},
                    { "TypeIdName",md.TypeIdName},
                    { "Title",md.Title},
                    { "PositionId",md.PositionId},
                    { "Job",md.Job},
                    { "JobName",md.JobName},
                    { "Location",md.Location},
                    { "LocationName",md.LocationName},
                    { "Wage",md.Wage},
                    { "TrialTime",md.TrialTime},
                    { "Num",md.Num},
                    { "Experience",md.Experience},
                    { "Diploma",md.Diploma},
                    { "Gender",md.Gender},
                    { "Age",md.Age},
                    { "Describe",md.Describe},
                    { "Interest",md.Interest},
                    { "Other",md.Other},
                    { "Folder",md.Folder},
                    { "EndDate",md.EndDate},
                    { "StaffIdUpdate",md.StaffIdUpdate},
                    { "UpdateDate",DateTime.Now}
                }
            };
            var robj = service.P(obj);
            return View(robj.Result);
        }
        public JsonResult DeleteRecruitPost(tbl_Recruitment_Post md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Updatetbl_Recruitment_Post", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id",md.Id }
                },
                _d = new Dictionary<string, object>
                {
                    { "Status",0},
                    { "StaffIdUpdate",md.StaffIdUpdate},
                    { "UpdateDate",DateTime.Now}
                }
            };
            var robj = service.P(obj);
            return Json(robj.Result);
        }
        public ActionResult Employer_Info(int id)
        {
            //Get ky nang
            var objSkill = new GCRequest
            {
                _a = "fGettbl_Recruitment_Post", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"UserId", id},
                    { "Status",1}
                },
                _f = String.Join(",", typeof(tbl_Recruitment_Post).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjSkill = service.P(objSkill);
            if (robjSkill.Result.Equals(1) && robjSkill.Records.Any())
            {
                var result = robjSkill.Records.ConvertToList<tbl_Recruitment_Post>();
                ViewBag.RecruitmentPost = result;
            }

            // Get thong tin
            var obj = new GCRequest
            {
                _a = "fGettbl_UserAuth_View00", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id",id }
                },
                _f = String.Join(",", typeof(tbl_UserAuth_View00).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj);
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_UserAuth_View00>();
                return View(result);
            }
            return View();
        }
    }
}