using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sgrc.DikizaCS.DAL.Client;
using sgrc.DikizaCS.DAL.Program;
using sgrc.DikizaCS.Utility;

namespace sgrc.DikizaCS.Areas.User.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IClientAppService _clientRepository;
        private readonly IProgramAppService _programRepository;

        public DashboardController()
        {
            _clientRepository = new ClientAppService();
            _programRepository = new ProgramAppService();
        }
        // GET: User/Dashboard
        public ActionResult Dashboard()
        {
            ViewBag.Program = "Dashboard";
            ViewBag.ClientCounter = _clientRepository.ClientCounter();
            ViewBag.ProgramCounter = _programRepository.ProgramsCounter();
            ViewBag.ProgramBreadcrumb = "dashboard";
            ViewBag.ProgramPageDesc = "admin dashboard.";
            ViewBag.ActiveOpen = "active open";
            ViewBag.Open = "open";
            ViewBag.Email = this.LoggedInEmail();
            ViewBag.UserId = this.LoggedInId();
            ViewBag.Fullname = this.LoggedInName();
            return View();
        }
    }
}