using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using sgrc.DikizaCS.DAL.Client;
using sgrc.DikizaCS.DAL.Program;
using sgrc.DikizaCS.DAL.User;
using sgrc.DikizaCS.Models;
using sgrc.DikizaCS.DAL.User.Dto;

namespace sgrc.DikizaCS.Controllers
{
    public class AccountController : Controller
    {
        private  readonly IUserAppService _userRepository;
        private readonly IProgramAppService _programRepository;
        private readonly IClientAppService _clientRepository;

     
        public AccountController()
        {
            _userRepository = new UserAppService();
            _programRepository = new ProgramAppService();
            _clientRepository = new ClientAppService();
        }

        IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Auth(string secureAuthenticationToken)
        {
            var result = _clientRepository.AccountConfirmation(secureAuthenticationToken);
            ViewBag.Result = result.Result.Status;
            return View(result);
        }

        public async Task<ActionResult> LoginUser(LoginModel user)
        {
            
            var authInput = new LoginInput()
            {
                Username = user.Username,
                Password = user.Password
            };
            if (ModelState.IsValid)
            {
                var loginResult = _userRepository.Login(authInput);

                if (loginResult.Result.User != null)
                {
                    if (loginResult.Result.User.IsAuthenticated)
                    {
                        if (loginResult.Result.User.IsAdmin)
                        {
                            //start admin
                            var fullname = loginResult.Result.User.Name + " " + loginResult.Result.User.Surname;
                            var email = loginResult.Result.User.Email;
                            var securityStamp = loginResult.Result.User.SecurityStamp;
                            var isAdmin = loginResult.Result.User.IsAdmin;
                            long id = loginResult.Result.User.Id;
                            var programs = _programRepository.ProgramsCounter();
                            var clients = _clientRepository.ClientCounter();
                            var claims = new List<Claim>();

                            claims.Add(new Claim("ProgramsCounter", programs.ToString()));
                            claims.Add(new Claim("ClientCounter", clients.ToString()));
                            claims.Add(new Claim(ClaimTypes.NameIdentifier, id.ToString()));
                            claims.Add(new Claim(ClaimTypes.Email, email));
                            claims.Add(new Claim(ClaimTypes.Role, isAdmin.ToString()));
                            claims.Add(new Claim(ClaimTypes.Name, fullname));
                            claims.Add(new Claim("securityStamp", securityStamp.ToString()));
                            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);
                            var currentUtc = new Microsoft.Owin.Infrastructure.SystemClock().UtcNow;
                            Authentication.SignIn(new AuthenticationProperties
                            {

                                IssuedUtc = currentUtc,
                                ExpiresUtc = currentUtc.Add(System.TimeSpan.FromHours(12)),
                            }, identity);
                            return Json(new
                            {
                                Success = true, //error
                                Redirect = "/User/Dashboard"
                            });
                            //End Admin
                        }
                        else
                        {

                            //start student
                            var fullname = loginResult.Result.User.Name + " " + loginResult.Result.User.Surname;
                            var email = loginResult.Result.User.Email;
                            var securityStamp = loginResult.Result.User.SecurityStamp;
                            var isAdmin = loginResult.Result.User.IsAdmin;
                            long id = loginResult.Result.User.Id;
                            long clientId = loginResult.Result.User.ClientId;
                            long programId = loginResult.Result.User.ProgramId;
                            
                            var claims = new List<Claim>();

                            
                            claims.Add(new Claim(ClaimTypes.NameIdentifier, id.ToString()));
                            claims.Add(new Claim("ClientId", clientId.ToString()));
                            claims.Add(new Claim("ProgramId", programId.ToString()));
                            claims.Add(new Claim(ClaimTypes.Email, email));
                            claims.Add(new Claim(ClaimTypes.Role, isAdmin.ToString()));
                            claims.Add(new Claim(ClaimTypes.Name, fullname));
                            claims.Add(new Claim("securityStamp", securityStamp.ToString()));
                            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);
                            var currentUtc = new Microsoft.Owin.Infrastructure.SystemClock().UtcNow;
                            Authentication.SignIn(new AuthenticationProperties
                            {

                                IssuedUtc = currentUtc,
                                ExpiresUtc = currentUtc.Add(System.TimeSpan.FromHours(12)),
                            }, identity);
                            return Json(new
                            {
                                Success = true, //error
                                Redirect = "/User/Student/Logbook"
                            });
                            //End Student

                        }

                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false, //error
                            Message = "Invalid Username/Password!"
                        });
                    }
                }
                if (loginResult.Result.ClientUser != null)
                {
                    if (loginResult.Result.ClientUser.IsAuthenticated)
                    {
                        //start client
                        var name = loginResult.Result.ClientUser.Name;
                        var email = loginResult.Result.ClientUser.Email;

                        var contactfullname = loginResult.Result.ClientUser.ContactName+ " "+loginResult.Result.ClientUser.ContactSurname;
                        var contactemail = loginResult.Result.ClientUser.ContactEmail;
                        long id = loginResult.Result.ClientUser.Id;
                       
                        var claims = new List<Claim>();
                        claims.Add(new Claim("CompanyName", name));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, id.ToString()));
                        claims.Add(new Claim(ClaimTypes.Email, email));
                        claims.Add(new Claim("ContactEmail", contactemail));
                        claims.Add(new Claim(ClaimTypes.Name, contactfullname));
                       
                        var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);
                        var currentUtc = new Microsoft.Owin.Infrastructure.SystemClock().UtcNow;
                        Authentication.SignIn(new AuthenticationProperties
                        {

                            IssuedUtc = currentUtc,
                            ExpiresUtc = currentUtc.Add(System.TimeSpan.FromHours(12)),
                        }, identity);
                        return Json(new
                        {
                            Success = true, //error
                            Redirect = "/User/Client"
                        });
                        //End Client
                    }
                    else
                    {
                        return Json(new
                        {
                            Success = false, //error
                            Message = "Invalid Username/Password!"
                        });
                    }
                } 
                else
                {
                    return Json(new
                    {
                        Success = false, //error
                        Message = "Invalid Username/Password!"
                    });
                }
          
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }
            throw new HttpResponseException(HttpStatusCode.Forbidden);
        }
        public ActionResult Logout()
        {
            Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Json(new
            {
                Redirect = "/"
            });
        }
    }
}