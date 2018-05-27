using System;
using System.Collections.Generic;
using System.Configuration;
using cModel;
using System.Linq;
using System.Reflection;
using System.Web;
using _1C7BEC44.Models;
using _1C7BEC44.Service;

namespace Website.Services
{
    public static class NewsService
    {
        public static List<tbl_News> GetNews()
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true);
            var obj = new GCRequest
            {
                _a = "fGettbl_News", //Action prefix f,p for get data; tbl_PT_MailBox is table name
                _c = new Dictionary<string, object>
                {
                    {"Status",1 }
                },
                _f = String.Join(",", typeof(tbl_News).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_News>();
                return result;
            }
            return null;
        }

        public static List<tbl_News> NewsDetail(int id)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true);
            var obj = new GCRequest
            {
                _a = "fGettbl_News", //Action prefix f,p for get data; tbl_PT_MailBox is table name
                _c = new Dictionary<string, object>
                {
                    {"Status",1 },
                    {"Id",id }
                },
                _f = String.Join(",", typeof(tbl_News).GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(c => c.Name))
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            if (robj.Result.Equals(1) && robj.Records.Any())
            {
                var result = robj.Records.ConvertToList<tbl_News>();
                return result;
            }
            return null;
        }

        public static object PostNews(tbl_News model)
        {
            if (model.ContentNews != null) model.ContentNews.Replace('\'', '\"');
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true);
            var obj = new GCRequest
            {
                _a = "Inserttbl_News", //Action prefix f,p for get data; tbl_PT_MailBox is table name
                _d = new Dictionary<string, object>
                {
                    {"NewsTypeId", model.NewsTypeId},
                    {"KieuString1", model.KieuString1},
                    {"NgayDeliver", DateTime.Now},
                    {"Name", model.Name},
                    {"ShortContentNews", model.ShortContentNews},
                    {"hinhanhImageSample", model.hinhanhImageSample},
                    {"TagName", model.TagName},
                    {"ContentNews", model.ContentNews},
                    {"CreateDate", DateTime.Now},
                    {"CreateStaffId", HttpContext.Current.Session["UserID"]}
                }
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            return robj.Result;
        }

        public static object UpdateNews(tbl_News model,string NgayDeliverConvert)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true);
            var obj = new GCRequest
            {
                _a = "Updatetbl_News", //Action prefix f,p for get data; tbl_PT_MailBox is table name
                _d = new Dictionary<string, object>
                {
                    {"NewsTypeId", model.NewsTypeId},
                    {"KieuString1", model.KieuString1},
                    {"NgayDeliver", WebsiteExtension.ConvertDate(NgayDeliverConvert)},
                    {"Name", model.Name},
                    {"ShortContentNews", model.ShortContentNews},
                    {"hinhanhImageSample", model.hinhanhImageSample},
                    {"TagName", model.TagName},
                    {"UpdateDate", DateTime.Now},
                    {"UpdateStaffId", HttpContext.Current.Session["UserID"]},
                    {"ContentNews", model.ContentNews}
                },
                _c = new Dictionary<string, object>
                {
                    {"Id", model.Id},
                    {"Status",1 }
                }
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            return robj.Result;
        }

        public static object DeleteNews(int id)
        {
            var service = new S(ConfigurationManager.ConnectionStrings["CotoidayCon"].ConnectionString, true);
            var obj = new GCRequest
            {
                _a = "Updatetbl_News", //Action prefix f,p for get data; tbl_PT_MailBox is table name
                _d = new Dictionary<string, object>
                {
                    {"Status", 0},
                    {"UpdateStaffId", HttpContext.Current.Session["UserID"]},
                    {"UpdateDate", DateTime.Now}
                },
                _c = new Dictionary<string, object>
                {
                    {"Id", id}
                }
            };
            var robj = service.P(obj); // {Result: 0 is failed, 1 is success, Records: List object array, TotalRecordCount: number of records, Message: error content }
            return robj.Result;
        }

    }
}