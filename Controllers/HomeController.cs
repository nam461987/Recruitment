using _1C7BEC44.Models;
using _1C7BEC44.Service;
using cModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Website.Services;
using Newtonsoft.Json;
using cotoiday_admin.Dtos;

namespace cotoiday_admin.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var fromDate = DateTime.Today.AddDays(-5);
            var toDate = DateTime.Today;
            DateTime dateNow = DateTime.Now;

            var obj = new GCRequest
            {
                _a = "fGettbl_UserAuth", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"Status", 1}
                },
                _f = String.Join(",", typeof(tbl_UserAuth).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj);

            var objSoluongNhaplieu = new GCRequest
            {
                _a = "fGettbl_UserAuth_SummaryByDay_View", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"CreatedDate",string.Format("$x >= '{0:yyyy-MM-dd}'", dateNow.AddDays(-5)) }
                },
                _f = String.Join(",", typeof(tbl_UserAuth_SummaryByDay_View).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjSoluongNhaplieu = service.P(objSoluongNhaplieu);

            if (robjSoluongNhaplieu.Result.Equals(1) && robjSoluongNhaplieu.Records.Any())
            {
                var result = robjSoluongNhaplieu.Records.ConvertToList<tbl_UserAuth_SummaryByDay_View>();

                ViewBag.DayList = result.Select(x => x.CreatedDate.ToString("dd/MM/yyyy")).Distinct().ToArray();


                //Get data by dict
                //var dict = new Dictionary<int, List<int>>();
                var dict = new Dictionary<int, Dictionary<DateTime, int>>();
                foreach (var item in result)
                {
                    var staffID = item.StaffId;
                    if (!dict.ContainsKey(staffID))
                    {
                        var d = new Dictionary<DateTime, int>();
                        for (var i = 0; i < (toDate - fromDate).TotalDays; i++)
                        {
                            d.Add(fromDate.AddDays(i), 0);
                        }
                        dict.Add(staffID, d);
                    }
                    dict[staffID][item.CreatedDate] = item.Total;
                }
                ViewBag.array = dict;

                //Get data by Json
                //var users = result.Select(c => c.CreatedDate).Distinct().ToList();
                //var data = new CreatingUserChartDto();
                //foreach(var item in result.GroupBy(c => c.StaffId).OrderBy(c => c.Key)){
                //    var serier = new List<int>();
                //    foreach (var user in users)
                //    {
                //    data.Labels.Add(user.ToString("dd/MM/yyyy"));
                //        var totalOfUser = item.FirstOrDefault(c => c.CreatedDate == user);
                //        if(totalOfUser != null)
                //        {
                //            serier.Add(totalOfUser.Total);
                //        }
                //        else
                //        {
                //            serier.Add(0);
                //        }
                //    }
                //    data.Seriers.Add(serier);
                //}
                //ViewBag.Chart = JsonConvert.SerializeObject(data);

                //var t = result.
                //var t = result.OrderBy(c => c.TypeId).ThenBy(c => c.CreatedDate)
                //    .GroupBy(c => c.TypeId).Select(g => new Dictionary<int, List<int>>()
                //    {
                //        g.Key,
                //        g.GroupBy(h => h.CreatedDate).Count()
                //    })
                //    .Select(c => c.Select(d => d)).ToList();

                //List<List<int>> array = new List<List<int>>();
                //foreach (var item in result)
                //{

                //}
            }

            return View(robj.TotalRecordCount);
        }
        public ActionResult Page_Deny()
        {
            return View();
        }
        public JsonResult GetNumAccCreatedIn5Days()
        {
            DateTime dateNow = DateTime.Now;
            var fromDate = DateTime.Today.AddDays(-10);
            var toDate = DateTime.Today;
            var objSoluongNhaplieu = new GCRequest
            {
                _a = "fGettbl_UserAuth_SummaryByDay_View", //Action prefix f,p for get data; gc_App is table name
                _c = new Dictionary<string, object>
                {
                    {"TypeId",2 },
                    {"CreatedDate",string.Format("$x >= '{0:yyyy-MM-dd}'", dateNow.AddDays(-10)) }
                },
                _f = String.Join(",", typeof(tbl_UserAuth_SummaryByDay_View).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robjSoluongNhaplieu = service.P(objSoluongNhaplieu);

            if (robjSoluongNhaplieu.Result.Equals(1) && robjSoluongNhaplieu.Records.Any())
            {
                var result = robjSoluongNhaplieu.Records.ConvertToList<tbl_UserAuth_SummaryByDay_View>();

                ViewBag.DayList = result.Select(x => x.CreatedDate.ToString("dd/MM/yyyy")).Distinct().ToArray();

                //Get data by dict
                var dict = new Dictionary<int, Dictionary<DateTime, int>>();
                foreach (var item in result)
                {
                    var staffID = item.StaffId;
                    if (!dict.ContainsKey(staffID))
                    {
                        var d = new Dictionary<DateTime, int>();
                        for (var i = 0; i < (toDate - fromDate).TotalDays; i++)
                        {
                            d.Add(fromDate.AddDays(i), 0);
                        }
                        dict.Add(staffID, d);
                    }
                    dict[staffID][item.CreatedDate] = item.Total;
                }

                var content = JsonConvert.SerializeObject(dict.Select(c => c.Value.Select(e => e.Value).ToList()).ToList());

                List<string> dateList = new List<string>();
                List<List<int>> seriers = new List<List<int>>();
                foreach (var item in dict)
                {
                    var serier = new List<int>();
                    foreach (var d in item.Value)
                    {
                        serier.Add(d.Value);
                    }
                    
                    seriers.Add(serier);
                }
                foreach (var item in dict.Take(1))
                {
                    foreach (var d in item.Value)
                    {
                        dateList.Add(d.Key.Date.ToString("dd/MM/yyyy"));
                    }
                }
                return Json(new { datelist = dateList, seriers = seriers,content = content });

                //Get data by Json
                //var users = result.Select(c => c.CreatedDate).Distinct().ToList();
                //var data = new CreatingUserChartDto();
                //foreach (var item in result.GroupBy(c => c.StaffId).OrderBy(c => c.Key))
                //{
                //    var serier = new List<int>();
                //    foreach (var user in users)
                //    {
                //        data.Labels.Add(user.ToString("dd/MM/yyyy"));
                //        var totalOfUser = item.FirstOrDefault(c => c.CreatedDate == user);
                //        if (totalOfUser != null)
                //        {
                //            serier.Add(totalOfUser.Total);
                //        }
                //        else
                //        {
                //            serier.Add(0);
                //        }
                //    }
                //    data.Seriers.Add(serier);
                //}
                //return Json(JsonConvert.SerializeObject(data));
            }
            return null;
        }
        public ActionResult Upload()
        {
            var file = Request.Files["Filedata"];
            Random r = new Random();
            string filename = r.Next().ToString() + "_" + file.FileName;

            //create folder by month
            string now = DateTime.Now.ToString("MMyyyy");
            string newFolder = @"D:\Project\Cotoiday\Cotoiday\Cotoiday\Content\" + now + "";
            //string newThumbFolder = Server.MapPath(@"~\Content\uploads\" + now + "\thumb");
            string savePath = "";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(newFolder))
                {
                    //save file to exists folder
                    savePath = @"D:\Project\Cotoiday\Cotoiday\Cotoiday\Content\" + now + "\\" + filename;
                    file.SaveAs(savePath);
                    return Content(Url.Content(@"D:\Project\Cotoiday\Cotoiday\Cotoiday\Content\" + now + "\\" + filename));
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(newFolder);
                DirectoryInfo dii = Directory.CreateDirectory(newFolder + "\\thumb");
                //save file to new folder
                savePath = @"D:\Project\Cotoiday\Cotoiday\Cotoiday\Content\" + now + "\\" + filename;
                file.SaveAs(savePath);

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            return Content(Url.Content(@"D:\Project\Cotoiday\Cotoiday\Cotoiday\Content\" + now + "\\" + filename));
        }
    }
}