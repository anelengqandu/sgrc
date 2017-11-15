using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sgrc.DikizaCS.DAL.Logbook;
using sgrc.DikizaCS.Utility;

namespace sgrc.DikizaCS.Areas.User.Controllers
{
    public class StudentController : Controller
    {

        private readonly ILogbookAppService _logbookRepository;
        public StudentController()
        {
            _logbookRepository = new LogbookAppService();
        }
        // GET: User/Student
        public ActionResult Index()
        {
            ViewBag.Program = "Student";
           
            ViewBag.ProgramBreadcrumb = "Student Logbook";
            ViewBag.ProgramPageDesc = "log your daily activities";
            ViewBag.ActiveOpen = "active open";
            ViewBag.Open = "open";
            ViewBag.Email = this.LoggedInEmail();
            ViewBag.UserId = this.LoggedInId();
            ViewBag.Fullname = this.LoggedInName();
            var data=_logbookRepository.GetAll(this.LoggedInId());
            return View(data.Result);
        }
    }
}