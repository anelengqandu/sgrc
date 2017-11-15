using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sgrc.DikizaCS.DAL.Client;
using sgrc.DikizaCS.DAL.User;
using sgrc.DikizaCS.DAL.Utils;
using sgrc.DikizaCS.Utility;

namespace sgrc.DikizaCS.Areas.User.Controllers
{
    public class ClientUserController : Controller
    {
        // GET: User/ClientUser
        private readonly IUserAppService _clientStudentRepository;
      
        public ClientUserController()
        {
            _clientStudentRepository = new UserAppService();
        }
        // GET: User/Student
        public ActionResult Index()
        {

            ViewBag.Program = "Client";
            ViewBag.ProgramBreadcrumb = "Client  Student";
            ViewBag.ProgramPageDesc = "list of client students";
            ViewBag.ActiveOpen = "active open";
            ViewBag.Open = "open";
            ViewBag.Email = this.LoggedInEmail();
            ViewBag.ClientUserId = this.LoggedInId();
            ViewBag.Fullname = this.LoggedInName();
            var data = _clientStudentRepository.GetByClient(this.LoggedInId());
            return View(data);
        }
    }
}