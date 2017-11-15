using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using sgrc.DikizaCS.DAL.Logbook;
using sgrc.DikizaCS.DAL.Utils;
using sgrc.DikizaCS.Utility;

namespace sgrc.DikizaCS.Areas.User.Controllers
{
    public class LogbookController : Controller
    {
        private readonly ILogbookAppService _logbookRepository;

        public LogbookController()
        {
            _logbookRepository = new LogbookAppService();
        }
        // GET: User/Logbook
        public ActionResult All(long studentId)
        {
            int rowCount = -1;
            PagingInfo paging = new PagingInfo
            {
                Skip = 0,
                Take = 10,
                SearchString = ""
            };
            ViewBag.Program = "Client";
            ViewBag.ProgramBreadcrumb = "Client  Student";
            ViewBag.ProgramPageDesc = "list of client students";
            ViewBag.ActiveOpen = "active open";
            ViewBag.Open = "open";
            ViewBag.Email = this.LoggedInEmail();
            ViewBag.ClientUserId = this.LoggedInId();
            ViewBag.Fullname = this.LoggedInName();
            var data = _logbookRepository.GetByStudent(ref paging,studentId);

            rowCount = paging.ResultCount;
           
            JavaScriptSerializer js = new JavaScriptSerializer();
            ViewBag.data = js.Serialize(new { aaData = data.ToList(), count = rowCount });



            return View(data);
        }

        public ActionResult New()
        {
            ViewBag.ProgramCounter = this.ProgramCounter();
            ViewBag.ClientCounter = this.ClientCounter();
            return View();
        }

        public ActionResult Edit()
        {
            ViewBag.ProgramCounter = this.ProgramCounter();
            return View();
        }
    }
}