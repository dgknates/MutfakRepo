using MvcLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace MvcLogin.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View(new Kullanıcılar());
        }
        [HttpPost]
        public ActionResult Index(Kullanıcılar model)
        {

            Kullanıcılar kullanıcı = StokKontrolEntitiesProvider.GetPersonByUserNameAndPasword(model.KullanıcıAdı,model.Parola);
            if (kullanıcı == default(Kullanıcılar))
            {
                ModelState.AddModelError("KullanıcıAdı", "Hatalı kullanıcı adı ya da parola");
                return View(model);
            }
            else
            {
                Session["login"] = kullanıcı;
                Session["UserObjectId"] = kullanıcı.ObjectId;
                Session["username"] = kullanıcı.KullanıcıAdı;
                Session["grup"] = kullanıcı.GrupId;
                return RedirectToAction("Homepage","Home");
            }
        }
    }
}


