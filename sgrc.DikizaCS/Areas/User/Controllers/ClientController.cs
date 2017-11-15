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
    public class ClientController : Controller
    {

        private readonly IClientAppService _clientRepository;
        private readonly IProgramAppService _programRepository;

        public ClientController()
        {
            _clientRepository = new ClientAppService();
            _programRepository = new ProgramAppService();
        }

        // GET: User/Client
        public ActionResult All()
        {
            ViewBag.Program = "Clients";
            ViewBag.ClientCounter = _clientRepository.ClientCounter();
            ViewBag.ProgramCounter = _programRepository.ProgramsCounter();
            ViewBag.ProgramBreadcrumb = "Clients";
            ViewBag.ProgramPageDesc = "create new clients,assign programs and students.";
            ViewBag.ActiveOpen = "active open";
            ViewBag.Open = "open";
            ViewBag.Email = this.LoggedInEmail();
            ViewBag.UserId = this.LoggedInId();
            ViewBag.Fullname = this.LoggedInName();
            var data = _clientRepository.GetAll();
            return View(data);
        }

        public ActionResult New()
        {
            ViewBag.Program = "Clients";
            ViewBag.ClientCounter = _clientRepository.ClientCounter();
            ViewBag.ProgramCounter = _programRepository.ProgramsCounter();
            ViewBag.ProgramBreadcrumb = "Create Program";
            ViewBag.ProgramPageDesc = "creating new program for students.";
            ViewBag.ActiveOpen = "active open";
            ViewBag.Open = "open";
            ViewBag.Email = this.LoggedInEmail();
            ViewBag.UserId = this.LoggedInId();
            ViewBag.Fullname = this.LoggedInName();
            return View();
        }

        public ActionResult Edit(long? id)
        {
            int rowCount = -1;
            PagingInfo paging = new PagingInfo
            {
                Skip = 0,
                Take = 10,
                SearchString = ""
            };

            ViewBag.Program = "Clients";
            ViewBag.ClientCounter = _clientRepository.ClientCounter();
            ViewBag.ProgramCounter = _programRepository.ProgramsCounter();
            ViewBag.ProgramBreadcrumb = "Edit Client";
            ViewBag.ProgramPageDesc = "edit client,create students and view logbooks.";
            ViewBag.ActiveOpen = "active open";
            ViewBag.Open = "open";
            ViewBag.Email = this.LoggedInEmail();
            ViewBag.UserId = this.LoggedInId();
            ViewBag.Id = id;
            ViewBag.Fullname = this.LoggedInName();
            var data = _clientRepository.GetClientData(ref paging, id);
            rowCount = paging.ResultCount;
            ViewBag.StudentCounter = data.Students.Count;
            JavaScriptSerializer js = new JavaScriptSerializer();
            ViewBag.students = js.Serialize(new { aaData = data.Students.ToList(), count = rowCount });
            ViewBag.clientPrograms = js.Serialize(new { aaData = data.ClientPrograms.ToList(), count = rowCount });


           
            return View(data);
        }
    }
}