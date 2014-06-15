using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Animais360.Models;

namespace Animais360.Controllers
{
    public class UserAreaProtegidaController : Controller
    {
        private Animais360Context db = new Animais360Context();

        //
        // GET: /UserAreaProtegida/

        public ActionResult Index()
        {
            return View(db.UserAreaProtegidas.ToList());
        }

        //
        // GET: /UserAreaProtegida/Details/5

        public ActionResult Details(int id = 0)
        {
            UserAreaProtegida userareaprotegida = db.UserAreaProtegidas.Find(id);
            if (userareaprotegida == null)
            {
                return HttpNotFound();
            }
            return View(userareaprotegida);
        }

        //
        // GET: /UserAreaProtegida/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserAreaProtegida/Create

        [HttpPost]
        public ActionResult Create(UserAreaProtegida userareaprotegida)
        {
            if (ModelState.IsValid)
            {
                db.UserAreaProtegidas.Add(userareaprotegida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userareaprotegida);
        }

        //
        // GET: /UserAreaProtegida/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserAreaProtegida userareaprotegida = db.UserAreaProtegidas.Find(id);
            if (userareaprotegida == null)
            {
                return HttpNotFound();
            }
            return View(userareaprotegida);
        }

        //
        // POST: /UserAreaProtegida/Edit/5

        [HttpPost]
        public ActionResult Edit(UserAreaProtegida userareaprotegida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userareaprotegida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userareaprotegida);
        }

        //
        // GET: /UserAreaProtegida/Delete/5

        public ActionResult Delete(int id = 0)
        {
            UserAreaProtegida userareaprotegida = db.UserAreaProtegidas.Find(id);
            if (userareaprotegida == null)
            {
                return HttpNotFound();
            }
            return View(userareaprotegida);
        }

        //
        // POST: /UserAreaProtegida/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAreaProtegida userareaprotegida = db.UserAreaProtegidas.Find(id);
            db.UserAreaProtegidas.Remove(userareaprotegida);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}