using _1C7BEC44.Models;
using cotoiday_admin.Common;
using NhaKhoaAdmin.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Services;

namespace cotoiday_admin.Controllers
{
    [ValidateInput(false)]
    [SessionAuthorize]
    public class NewsController : BaseController
    {
        // GET: News
        [HttpGet]
        public ActionResult AddNews()
        {
            ViewBag.Success = 2;
            return View();
        }

        [HttpPost]
        public ActionResult AddNews(tbl_News md)
        {
            var model = NewsService.PostNews(md);
            ViewBag.Success = model;
            return View();
        }
        [HttpGet]
        public ActionResult EditNews(int id)
        {
            var model = NewsService.NewsDetail(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditNews(tbl_News md, string NgayDeliverConvert)
        {
            var model = NewsService.UpdateNews(md, NgayDeliverConvert);

            return RedirectToAction("ManageNews", "News");
        }
        public ActionResult ManageNews()
        {
            var model = NewsService.GetNews();
            return View(model);
        }
        public ActionResult NewsDelete(int id)
        {
            var model = NewsService.DeleteNews(id);
            return RedirectToAction("ManageNews", "News");
        }
        public JsonResult Upload()
        {
            var file = Request.Files["Filedata"];
            Random r = new Random();
            string filename = r.Next().ToString() + "_" + file.FileName;

            //create folder by month
            string now = DateTime.Now.ToString("MMyyyy");
            string newFolder = @"" + Strings.UploadFolderRoot + "" + now + "";
            //string newThumbFolder = Server.MapPath(@"~\Content\uploads\" + now + "\thumb");
            string savePath = "";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(newFolder))
                {
                    //save file to exists folder
                    savePath = @"" + Strings.UploadFolderRoot + "" + now + "\\" + filename;
                    file.SaveAs(savePath);
                    return Json("/Content/uploads/News/" + now + "/" + filename);
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(newFolder);
                DirectoryInfo dii = Directory.CreateDirectory(newFolder + "\\thumb");
                //save file to new folder
                savePath = @"" + Strings.UploadFolderRoot + "" + now + "\\" + filename;
                file.SaveAs(savePath);

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            //return Content(Url.Content(@"D:\Project\cotoiday v2.0\cotoiday\cotoiday\Content\uploads\" + now + "\\" + filename));
            return Json("/Content/uploads/News/" + now + "/" + filename);
        }
    }
}