using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TontineASP.Models;
using TontineASP.Services;

namespace TontineASP.Controllers
{
    public class ConnectController : Controller
    {
        // GET: Connect
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            Authentication useCase = new Authentication(new AuthenficateCommand(model.Email, model.Password));
            var user = useCase.Execute();

            if (user == null)
            {
                model.IsError = true;
                model.Message = "L'email ou le mot de passe est incorrect";
                return View(model);
            }
            return RedirectToAction("Index", "Connect", new { fullname = user.Fullname });

        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {

            Insert_Authenticate useCase = new Insert_Authenticate(new AuthenticateInsert(model.Fullname, model.Email, model.Password, model.Telephone));
            var user = useCase.Execute();

            if (user != null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Accueil(HomeModel model)
        {
            return View(model);
        }

        public ActionResult Reunion()
        {
            return View();
        }
    }
}