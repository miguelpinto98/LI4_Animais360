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
    public class AreaProtegidaController : Controller
    {
        private Animais360Context db = new Animais360Context();

        //
        // GET: /AreaProtegida/

        public ActionResult Index() {
            ViewBag.Continentes = db.Continentes.ToList();
            ViewBag.Paises = db.Pais.ToList();

            return View(db.AreaProtegidas.ToList());
        }

        //
        // GET: /AreaProtegida/Details/5

        public ActionResult Details(int id = 0)
        {
            AreaProtegida areaprotegida = db.AreaProtegidas.Find(id);
            if (areaprotegida == null)
            {
                return HttpNotFound();
            }
            return View(areaprotegida);
        }

        //
        // GET: /AreaProtegida/Create

        public ActionResult Create() {
            ViewBag.Continentes = db.Continentes.ToList();
            ViewBag.Paises = db.Pais.ToList();

            return View();
        }

        //
        // POST: /AreaProtegida/Create

        [HttpPost]
        public ActionResult Create(AreaProtegida areaprotegida)
        {
            if (ModelState.IsValid)
            {
                db.AreaProtegidas.Add(areaprotegida);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(areaprotegida);
        }

        //
        // GET: /AreaProtegida/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AreaProtegida areaprotegida = db.AreaProtegidas.Find(id);
            if (areaprotegida == null)
            {
                return HttpNotFound();
            }
            return View(areaprotegida);
        }

        //
        // POST: /AreaProtegida/Edit/5

        [HttpPost]
        public ActionResult Edit(AreaProtegida areaprotegida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areaprotegida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(areaprotegida);
        }

        //
        // GET: /AreaProtegida/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AreaProtegida areaprotegida = db.AreaProtegidas.Find(id);
            if (areaprotegida == null)
            {
                return HttpNotFound();
            }
            return View(areaprotegida);
        }

        //
        // POST: /AreaProtegida/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            AreaProtegida areaprotegida = db.AreaProtegidas.Find(id);
            db.AreaProtegidas.Remove(areaprotegida);
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