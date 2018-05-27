using cotoiday_admin.Common;
using CotoidayCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cotoiday_admin.Controllers
{
    [SessionAuthorize]
    public class RecruitmentController : Controller
    {
        // GET: Recruitment
        public ActionResult RecruitmentList()
        {
            List<Recruitment_Post_View01> JobList = Recruitment_Post_View01.Query("Where Status = 1 AND Active=1").ToList();
            ViewBag.JobList = JobList;
            return View();
        }
        public ActionResult WaitingRecruitmentList()
        {
            List<Recruitment_Post_View01> JobList = Recruitment_Post_View01.Query("Where Active=0 AND Status = 1").ToList();
            ViewBag.JobList = JobList;
            return View();
        }
        public ActionResult DeletedRecruitmentList()
        {
            List<Recruitment_Post_View01> JobList = Recruitment_Post_View01.Query("Where Status = 0").ToList();
            ViewBag.JobList = JobList;
            return View();
        }
        public ActionResult RecruitmentDetail(int id)
        {
            List<Recruitment_Post_View01> JobDetail = Recruitment_Post_View01.Query("Where Id=@0", id).ToList();
            if (JobDetail != null && JobDetail.Any())
            {
                ViewBag.JobDetail = JobDetail;

                return View();
            }
            return Redirect("/Error/Error");
        }
        public ActionResult DeleteRecruitment(int id)
        {
            Recruitment_Post Job = Recruitment_Post.SingleOrDefault("Where Id=@0 AND Status=1", id);
            try
            {
                Job.Status = 0;
                Job.StaffIdUpdate = Convert.ToInt32(Session["UserId"]);
                Job.UpdateDate = DateTime.Now;
                Job.Save();
            }
            catch
            {
                return Redirect("/Error/Error");
            }
            return Redirect("/Recruitment/DeletedRecruitmentList");
        }
        public ActionResult ActiveRecruitment(int id)
        {
            Recruitment_Post Job = Recruitment_Post.SingleOrDefault("Where Id=@0", id);
            try
            {
                Job.Active = 1;
                Job.StaffIdUpdate = Convert.ToInt32(Session["UserId"]);
                Job.UpdateDate = DateTime.Now;
                Job.Save();
            }
            catch
            {
                return Redirect("/Error/Error");
            }
            return Redirect("/Recruitment/RecruitmentDetail/" + id);
        }
        public ActionResult _RecruitmentRightSide(int id)
        {
            List<Recruitment_Post_View01> JobList = Recruitment_Post_View01.Query("Select Top 2 * From tbl_Recruitment_Post_View01 Where Id <> @0 AND Active =0 AND Status = 1", id).ToList();
            if (JobList != null && JobList.Any())
            {
                ViewBag.JobList = JobList;
            }
            return PartialView();
        }
    }
}