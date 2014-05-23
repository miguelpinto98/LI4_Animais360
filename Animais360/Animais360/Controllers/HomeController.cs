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
        public ActionResult Index() {

            if (!Request.IsAuthenticated) {
                List<User> utilizadores = db.Users.Where(u => u.Estado == 1).ToList();
                ViewBag.numero = db.Users.Count();
                ViewBag.last = db.Users.OrderByDescending(u => u.DataRegisto).ToList();
                int voltas = 0;
                foreach (User u in utilizadores)
                {
                    voltas = voltas + u.NrVoltas;
                }
                ViewBag.nVoltas = voltas;
                ViewBag.conquistador = "Não Definido";
                return View();
            } else {
                if(ViewBag.Tipo == null || ViewBag.Tipo == 1) {
                    return RedirectToAction("Gerir", "User");
                } else {
                    return RedirectToAction("Index", "User");
                }
            }
        }
    }
}
