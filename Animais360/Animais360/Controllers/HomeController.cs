using Animais360.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Animais360.Controllers
{
    public class HomeController : Controller
    {
        private Animais360Context db = new Animais360Context();
        public ActionResult Index()
        {
            
            ViewBag.numero = db.Users.Count();
            ViewBag.last = "Não Definido";
            return View();
        }
    }
}
