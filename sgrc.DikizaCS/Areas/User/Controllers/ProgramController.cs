using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using sgrc.DikizaCS.DAL.Client;
using sgrc.DikizaCS.DAL.Program;
using sgrc.DikizaCS.DAL.Utils;
using sgrc.DikizaCS.Utility;

namespace sgrc.DikizaCS.Areas.User.Controllers
{
    public class ProgramController : Controller
    {
        private readonly IProgramAppService _programRepository;
        private readonly IClientAppService _clientRepository;
        public ProgramController()
        {
            _programRepository = new ProgramAppService();
            _clientRepository = new ClientAppService();
        }
        // GET: User/Program
        public ActionResult All()
        {

            int rowCount = -1;
            PagingInfo paging = new PagingInfo();
            paging.Skip = 0;
            paging.Take = 10;
            paging.SearchString = "";
            ViewBag.ClientCounter = _clientRepository.ClientCounter();
            ViewBag.ProgramCounter = _programRepository.ProgramsCounter();
            ViewBag.Program = "Program";
            ViewBag.ProgramBreadcrumb = "All Programs";
            ViewBag.ProgramPageDesc = "list of all programs.";
            ViewBag.ActiveOpen = "active open";
            ViewBag.Open = "open";
            ViewBag.Email = this.LoggedInEmail();
            ViewBag.UserId= this.LoggedInId();
            ViewBag.Fullname = this.LoggedInName();


            var data = _programRepository.GetPrograms(ref paging);

         

            JavaScriptSerializer js = new JavaScriptSerializer();
            ViewBag.data = js.Serialize(new { aaData = data.ToList(), count = data.Count });

            return View(data.ToList());
        }
        public ActionResult New()
        {
            ViewBag.Program = "Program";
            ViewBag.ClientCounter = _clientRepository.ClientCounter();
            ViewBag.ProgramCounter = _programRepository.ProgramsCounter();
            ViewBag.ProgramBreadcrumb = "Create Program";
            ViewBag.ProgramPageDesc = "creating new program for students.";
            ViewBag.ActiveOpen="active open";
            ViewBag.Open="open";
            ViewBag.Email = this.LoggedInEmail();
            ViewBag.UserId = this.LoggedInId();
            ViewBag.Fullname = this.LoggedInName();
            return View();
        }
        public ActionResult Edit(long ProgramId)
        {
           
            ViewBag.Program = "Program";
            ViewBag.ClientCounter = _clientRepository.ClientCounter();
            ViewBag.ProgramCounter = _programRepository.ProgramsCounter();
            ViewBag.ProgramBreadcrumb = "Edit Program";
            ViewBag.ProgramPageDesc = "update program details.";
            ViewBag.ActiveOpen = "active open";
            ViewBag.Open = "open";
            ViewBag.Email = this.LoggedInEmail();
            ViewBag.UserId = this.LoggedInId();
            ViewBag.ProgramId = ProgramId;
            ViewBag.Fullname = this.LoggedInName();
            var data = _programRepository.GetAsync(ProgramId);

            return View(data);
        }
    }
}