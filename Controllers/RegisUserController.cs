using _1C7BEC44.Models;
using _1C7BEC44.Service;
using cModel;
using cotoiday_admin.Common;
using cotoiday_admin.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Website.Services;

namespace cotoiday_admin.Controllers
{
    public class RegisUserController : BaseController
    {
        //public IService service { get; set; }

        // GET: RegisUser
        [HttpGet]
        [HasCredential(Role = "Customer_Account_Create")]
        public ActionResult CreateAccount()
        {
            ViewBag.result = 2;
            ViewBag.type = 0;
            return View();
        }
        [HttpPost]
        [HasCredential(Role = "Customer_Account_Create")]
        public ActionResult CreateAccount(tbl_UserAuth md)
        {
            var convertPass = WebsiteExtension.EncryptPassword(md.PasswordHash);
            //var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Inserttbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                _d = new Dictionary<string, object>
                {
                    {"TypeId", md.TypeId},
                    {"UserName", md.UserName.ToLower()},
                    {"PasswordHash", convertPass},
                    {"StaffId",md.StaffId },
                    {"Status",1 },
                    {"CreatedDate",DateTime.Now }
                }
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            ViewBag.result = robj.Result;
            ViewBag.type = md.TypeId;
            return View(robj.Records[0][0]);
        }
        public JsonResult CheckUser(tbl_UserAuth md)
        {
            //var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "fGettbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
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
        [HttpGet]
        public ActionResult Account(int page = 1)
        {
            ViewBag.RecordNum = null;
            //var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            var obj = new GCRequest();

            var staffId = int.Parse(Session["UserId"].ToString());
            if (staffId == 3 || staffId == 4)
            {
                obj = new GCRequest
                {
                    _a = "pGettbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                    _c = new Dictionary<string, object>
                    {
                        {"TypeId", 2},
                        {"Status", 1},
                        {"StaffId",string.Format("$x > 0") }
                    },
                    _od = new Dictionary<string, string>()
                    {
                        {"CreatedDate", "DESC" }
                    },
                    _os = (page - 1) * 10,
                    _lm = 10,
                    _f = String.Join(",", typeof(tbl_UserAuth).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
                };
            }
            else
            {
                obj = new GCRequest
                {
                    _a = "pGettbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                    _c = new Dictionary<string, object>
                {
                    {"Status", 1},
                    {"StaffId",string.Format("$x > 0") }
                },
                    _od = new Dictionary<string, string>()
                    {
                        {"CreatedDate", "DESC" }
                    },
                    _os = (page - 1) * 10,
                    _lm = 10,
                    _f = String.Join(",", typeof(tbl_UserAuth).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
                };
            }

            var robj = service.P(obj);
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_UserAuth>();
                result = result.OrderByDescending(x => x.CreatedDate).ToList();

                var totalPage = Convert.ToInt32(Math.Ceiling((double)robj.TotalRecordCount / 10));
                ViewBag.CurrentPage = page;
                ViewBag.TotalPage = totalPage;

                return View(result);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Account(string searchfield)
        {
            //var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            var obj = new GCRequest();

            var staffId = int.Parse(Session["UserId"].ToString());

            if (staffId == 3 || staffId == 4)
            {
                obj = new GCRequest
                {
                    _a = "fGettbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                    _c = new Dictionary<string, object>
                {
                    { "UserName",string.Format("(PATINDEX(N'%{0}%', $x) > 0 OR PATINDEX(N'%{0}%', Email) > 0 OR PATINDEX(N'%{0}%', PhoneNumber) > 0)",searchfield) },
                    {"TypeId",2 },
                    { "Status", 1},
                    {"StaffId",string.Format("$x > 0") }
                },
                    _f = String.Join(",", typeof(tbl_UserAuth).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
                };
            }
            else
            {
                obj = new GCRequest
                {
                    _a = "fGettbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                    _c = new Dictionary<string, object>
                {
                    { "UserName",string.Format("(PATINDEX(N'%{0}%', $x) > 0 OR PATINDEX(N'%{0}%', Email) > 0 OR PATINDEX(N'%{0}%', PhoneNumber) > 0)",searchfield) },
                    {"Status", 1},
                    {"StaffId",string.Format("$x > 0") }
                },
                    _f = String.Join(",", typeof(tbl_UserAuth).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
                };
            }

            var robj = service.P(obj);
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_UserAuth>();
                result = result.OrderByDescending(x => x.CreatedDate).ToList();
                ViewBag.RecordNum = robj.TotalRecordCount;

                ViewBag.CurrentPage = 1;
                ViewBag.TotalPage = 0;

                return View(result);
            }
            ViewBag.RecordNum = 0;
            return View();
        }
        [HttpGet]
        public ActionResult AccountNotActive(int page = 1)
        {
            ViewBag.RecordNum = null;
            //var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            var obj = new GCRequest();

            var groupId = int.Parse(Session["GroupId"].ToString());
            if (groupId == 3)
            {
                obj = new GCRequest
                {
                    _a = "pGettbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                    _c = new Dictionary<string, object>
                    {
                        {"TypeId", 2},
                        {"Active", 0},
                        {"StaffId",string.Format("$x > 0") }
                    },
                    _od = new Dictionary<string, string>()
                    {
                        {"CreatedDate", "DESC" }
                    },
                    _os = (page - 1) * 10,
                    _lm = 10,
                    _f = String.Join(",", typeof(tbl_UserAuth).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
                };
            }
            else
            {
                obj = new GCRequest
                {
                    _a = "pGettbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                    _c = new Dictionary<string, object>
                {
                    {"Active", 0},
                    {"StaffId",string.Format("$x > 0") }
                },
                    _od = new Dictionary<string, string>()
                    {
                        {"CreatedDate", "DESC" }
                    },
                    _os = (page - 1) * 10,
                    _lm = 10,
                    _f = String.Join(",", typeof(tbl_UserAuth).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
                };
            }

            var robj = service.P(obj);
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_UserAuth>();
                result = result.OrderByDescending(x => x.CreatedDate).ToList();

                var totalPage = Convert.ToInt32(Math.Ceiling((double)robj.TotalRecordCount / 10));
                ViewBag.CurrentPage = page;
                ViewBag.TotalPage = totalPage;

                return View(result);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AccountNotActive(string searchfield)
        {
            //var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            var obj = new GCRequest();

            var groupId = int.Parse(Session["GroupId"].ToString());

            if (groupId == 3)
            {
                obj = new GCRequest
                {
                    _a = "fGettbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                    _c = new Dictionary<string, object>
                {
                    { "UserName",string.Format("(PATINDEX(N'%{0}%', $x) > 0 OR PATINDEX(N'%{0}%', Email) > 0 OR PATINDEX(N'%{0}%', PhoneNumber) > 0)",searchfield) },
                    {"TypeId",2 },
                    {"Active", 0},
                    {"StaffId",string.Format("$x > 0") }
                },
                    _f = String.Join(",", typeof(tbl_UserAuth).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
                };
            }
            else
            {
                obj = new GCRequest
                {
                    _a = "fGettbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                    _c = new Dictionary<string, object>
                {
                    { "UserName",string.Format("(PATINDEX(N'%{0}%', $x) > 0 OR PATINDEX(N'%{0}%', Email) > 0 OR PATINDEX(N'%{0}%', PhoneNumber) > 0)",searchfield) },
                    {"Active", 0},
                    {"StaffId",string.Format("$x > 0") }
                },
                    _f = String.Join(",", typeof(tbl_UserAuth).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
                };
            }

            var robj = service.P(obj);
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_UserAuth>();
                result = result.OrderByDescending(x => x.CreatedDate).ToList();
                ViewBag.RecordNum = robj.TotalRecordCount;

                ViewBag.CurrentPage = 1;
                ViewBag.TotalPage = 0;

                return View(result);
            }
            ViewBag.RecordNum = 0;
            return View();
        }
        public ActionResult Account_Detail(int? id)
        {
            var staffId = int.Parse(Session["UserId"].ToString());
            if (!id.HasValue)
            {
                return Redirect("/");
            }
            //var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            //Get account
            var objType = new GCRequest
            {
                _a = "fGettbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id", id},
                    { "Status",1}
                },
                _f = "TypeId"
            };
            var robjType = service.P(objType);
            if (int.Parse(robjType.Records[0][0].ToString()) == 1)
            {
                return Redirect("/");
            }

            //Get Experience List
            var objKinhNghiem = new GCRequest
            {
                _a = "fGettbl_User_Experience_View01", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"UserId", id},
                    { "Status",1}
                },
                _f = String.Join(",", typeof(tbl_User_Experience_View).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjKinhNghiem = service.P(objKinhNghiem);
            if (robjKinhNghiem.Result.Equals(1) && robjKinhNghiem.Records.Any())
            {
                var result = robjKinhNghiem.Records.ConvertToList<tbl_User_Experience_View>();
                ViewBag.KinhNghiemList = result;
            }
            //------------------------------------------------

            //Get Job List
            var obj = new GCRequest
            {
                _a = "fGettbl_Job", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"NganhId", string.Format("$x is Null")},
                    { "Status",1}
                },
                _f = String.Join(",", typeof(tbl_Job).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj);
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_Job>();
                return View(result);
            }
            //-----------------------------------
            return View();
        }
        //[HasCredential(Role = "Customer_Account_Create")]
        [Test]
        public JsonResult DeleteAccount(tbl_UserAuth md)
        {
            //var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            var obj = new GCRequest
            {
                _a = "Updatetbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
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
        public JsonResult GetCongViecByNganhId(int nganhId)
        {
            //var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "fGettbl_Job", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"NganhId", nganhId},
                    { "Status",1}
                },
                _f = String.Join(",", typeof(tbl_Job).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj);
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_Job>();
                return Json(result);
            }
            return Json(0);
        }
        public JsonResult AddKinhNghiem(tbl_User_Experience md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            // Check kinh nghiem
            var objCheck = new GCRequest
            {
                _a = "fGettbl_User_Experience", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"UserId", md.UserId},
                    { "NganhId",md.NganhId},
                    {"KieuString1",md.KieuString1 }
                    //{ "LoaiViecId",md.LoaiViecId}
                },
                _f = String.Join(",", typeof(tbl_User_Experience).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjCheck = service.P(objCheck);
            //-----------------------------------------------------

            // If exist : Update
            if (robjCheck.TotalRecordCount > 0)
            {
                var obj = new GCRequest
                {
                    _a = "Updatetbl_User_Experience", //Action prefix f,p for get data; gc_App is table name
                    _c = new Dictionary<string, object>
                    {
                        {"UserId", md.UserId},
                        { "NganhId",md.NganhId},
                        {"KieuString1",md.KieuString1 }
                        //{ "LoaiViecId",md.LoaiViecId}
                    },
                    _d = new Dictionary<string, object>
                    {
                        { "NumOfYear",md.NumOfYear},
                        { "Status",1},
                        { "UpdateDate",DateTime.Now}
                    }
                };
                var robj = service.P(obj);
                return Json(robj.Result);
            }
            // If null : Insert
            else {
                var obj = new GCRequest
                {
                    _a = "Inserttbl_User_Experience", //Action prefix f,p for get data; gc_App is table name
                    _d = new Dictionary<string, object>
                    {
                        {"UserId", md.UserId},
                        { "NganhId",md.NganhId},
                        {"KieuString1",md.KieuString1 },
                        //{ "LoaiViecId",md.LoaiViecId},
                        { "NumOfYear",md.NumOfYear},
                        { "CreateDate",DateTime.Now}
                    }
                };
                var robj = service.P(obj);
                return Json(robj.Result);
            }
            //----------------------------------------------
        }
        public JsonResult GetKinhNghiem(int UserId)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            //Get Experience List
            var objKinhNghiem = new GCRequest
            {
                _a = "fGettbl_User_Experience_View01", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"UserId", UserId},
                    { "Status",1}
                },
                _f = String.Join(",", typeof(tbl_User_Experience_View).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjKinhNghiem = service.P(objKinhNghiem);
            if (robjKinhNghiem.Result.Equals(1) && robjKinhNghiem.Records.Any())
            {
                var result = robjKinhNghiem.Records.ConvertToList<tbl_User_Experience_View>();
                return Json(result);
            }
            //------------------------------------------------
            return Json(0);
        }
        public JsonResult DeleteKinhNghiem(int id)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Updatetbl_User_Experience", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                    {
                        {"Id", id}
                    },
                _d = new Dictionary<string, object>
                    {
                        { "Status",0}
                    }
            };
            var robj = service.P(obj);
            return Json(robj.Result);
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
        public JsonResult UpdateInforUser(tbl_UserAuth md)
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
                        { "Nationality",md.Nationality},
                        { "Gender",md.Gender},
                        { "CityId",md.CityId},
                        { "Cmnd",md.Cmnd},
                        { "MaritalStatus",md.MaritalStatus},
                        { "Height",md.Height},
                        { "Weight",md.Weight},
                        { "Address",md.Address},
                        { "Email",md.Email},
                        { "PhoneNumber",md.PhoneNumber},
                        { "Criminal",md.Criminal},
                        { "CriminalReason",md.CriminalReason},
                        { "ModifiedDate",DateTime.Now}
                    }
            };
            var robj = service.P(obj);
            return Json(robj.Result);
        }
        public ActionResult _ProjectTabPartial(int id)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            //Get Project
            var obj = new GCRequest
            {
                _a = "fGettbl_User_Project_View00", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"UserId", id},
                    { "Status",1}
                },
                _f = String.Join(",", typeof(tbl_User_Project_View).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj);
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_User_Project_View>();
                ViewBag.Project = result;
            }
            //---------------------------------------------

            //Get Experience List
            var objKinhNghiem = new GCRequest
            {
                _a = "fGettbl_User_Experience_View01", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"UserId", id},
                    { "Status",1}
                },
                _f = String.Join(",", typeof(tbl_User_Experience_View).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjKinhNghiem = service.P(objKinhNghiem);
            if (robjKinhNghiem.Result.Equals(1) && robjKinhNghiem.Records.Any())
            {
                var result = robjKinhNghiem.Records.ConvertToList<tbl_User_Experience_View>();
                return PartialView(result);
            }
            //------------------------------------------------
            return PartialView();
        }
        public JsonResult AddProject(tbl_User_Project md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Inserttbl_User_Project", //Action prefix f,p for get data; gc_App is table name
                _d = new Dictionary<string, object>
                    {
                        {"UserId", md.UserId},
                        { "JobId",md.JobId},
                        { "Name",md.Name},
                        { "Position",md.Position},
                        { "Partner",md.Partner},
                        { "FromDate",md.FromDate},
                        { "ToDate",md.ToDate},
                        { "Note",md.Note},
                        { "ProjectPosition",md.ProjectPosition},
                        { "ProjectFromDate",md.ProjectFromDate},
                        { "ProjectToDate",md.ProjectToDate},
                        { "Image",md.Image},
                        { "CreateDate",DateTime.Now}
                    }
            };
            var robj = service.P(obj);
            return Json(robj.Result);
        }
        public JsonResult DeleteProject(int id)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Updatetbl_User_Project", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                    {
                        {"Id", id}
                    },
                _d = new Dictionary<string, object>
                    {
                        { "Status",0}
                    }
            };
            var robj = service.P(obj);
            return Json(robj.Result);
        }
        public JsonResult GetProjectById(int ID)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            //Get Project
            var obj = new GCRequest
            {
                _a = "fGettbl_User_Project_View00", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id", ID},
                    { "Status",1}
                },
                _f = String.Join(",", typeof(tbl_User_Project_View).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj);
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_User_Project_View>();
                return Json(result);
            }
            return Json(0);
        }
        public JsonResult UpdateProject(tbl_User_Project md)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id
            var obj = new GCRequest
            {
                _a = "Updatetbl_User_Project", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id",md.Id }
                },
                _d = new Dictionary<string, object>
                    {
                        { "JobId",md.JobId},
                        { "Name",md.Name},
                        { "Position",md.Position},
                        { "Partner",md.Partner},
                        { "FromDate",md.FromDate},
                        { "ToDate",md.ToDate},
                        { "Note",md.Note},
                        { "Image",md.Image},
                        { "UpdateDate",DateTime.Now}
                    }
            };
            var robj = service.P(obj);
            return Json(robj.Result);
        }
        public ActionResult Seeker_Info(int id)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true); //isDebug = true -> show error message in response object, uid is logged user id

            //Get ky nang
            var objSkill = new GCRequest
            {
                _a = "fGettbl_User_Experience_View01", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"UserId", id},
                    { "Status",1}
                },
                _f = String.Join(",", typeof(tbl_User_Experience_View).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjSkill = service.P(objSkill);
            if (robjSkill.Result.Equals(1) && robjSkill.Records.Any())
            {
                var result = robjSkill.Records.ConvertToList<tbl_User_Experience_View>();
                ViewBag.Skill = result;
            }

            // Get kinh nghiem
            var objExp = new GCRequest
            {
                _a = "fGettbl_User_Project_View00", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"UserId",id },
                    {"Status",1 }
                },
                _f = String.Join(",", typeof(tbl_User_Project_View).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjExp = service.P(objExp);
            if (robjExp.Result.Equals(1) && robjExp.Records.Any())
            {
                var result = robjExp.Records.ConvertToList<tbl_User_Project_View>();
                ViewBag.Exp = result;
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
        public JsonResult ReSendActiveEmail(tbl_UserAuth md)
        {
            string sGuid = Guid.NewGuid().ToString();
            var obj = new GCRequest
            {
                _a = "Updatetbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Id", md.Id},
                    {"UserName", md.UserName},
                    {"Active", 0},
                },
                _d = new Dictionary<string, object>
                {
                    {"TimeZone", sGuid},
                    {"CreatedDate", DateTime.Now},
                }
            };
            var robj = service.P(obj);

            if (int.Parse(robj.Records[0][0].ToString()) == 1)
            {
                string confirmCode = Convert.ToBase64String(Encoding.Unicode.GetBytes(String.Format("user={0}&id={1}&code={2}&date={3}", md.UserName, md.Id, sGuid, DateTime.Now)));
                var urlConfirmLink = "http://cotoiday.vn/RegisterAccount/ConfirmPage?token=" + confirmCode;
                var resendResult = SendConfirmMail.SendEmail(urlConfirmLink, md.Email);
                if (resendResult)
                {
                    return Json(1);
                }
            }

            return Json(0);
        }
    }
}