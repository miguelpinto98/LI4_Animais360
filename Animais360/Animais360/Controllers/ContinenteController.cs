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
    public class ContinenteController : Controller
    {
        private Animais360Context db = new Animais360Context();

        //
        // GET: /Continente/

        public ActionResult Index()
        {
            return View(db.Continentes.ToList());
        }

        //
        // GET: /Continente/Details/5

        public ActionResult Details(int id = 0)
        {
            Continente continente = db.Continentes.Find(id);
            if (continente == null)
            {
                return HttpNotFound();
            }
            return View(continente);
        }

        //
        // GET: /Continente/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Continente/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Continente continente)
        {
            if (ModelState.IsValid)
            {
                db.Continentes.Add(continente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(continente);
        }

        //
        // GET: /Continente/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Continente continente = db.Continentes.Find(id);
            if (continente == null)
            {
                return HttpNotFound();
            }
            return View(continente);
        }

        //
        // POST: /Continente/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Continente continente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(continente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(continente);
        }

        //
        // GET: /Continente/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Continente continente = db.Continentes.Find(id);
            if (continente == null)
            {
                return HttpNotFound();
            }
            return View(continente);
        }

        //
        // POST: /Continente/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Continente continente = db.Continentes.Find(id);
            db.Continentes.Remove(continente);
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