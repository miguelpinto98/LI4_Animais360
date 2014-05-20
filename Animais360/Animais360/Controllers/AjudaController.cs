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
    public class AjudaController : Controller
    {
        private Animais360Context db = new Animais360Context();

        //
        // GET: /Ajuda/

        public ActionResult Index()
        {
            return View(db.Ajudas.ToList());
        }

        //
        // GET: /Ajuda/Details/5

        public ActionResult Details(int id = 0)
        {
            Ajuda ajuda = db.Ajudas.Find(id);
            if (ajuda == null)
            {
                return HttpNotFound();
            }
            return View(ajuda);
        }

        //
        // GET: /Ajuda/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Ajuda/Create

        [HttpPost]
        public ActionResult Create(Ajuda ajuda)
        {
            if (ModelState.IsValid)
            {
                db.Ajudas.Add(ajuda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ajuda);
        }

        //
        // GET: /Ajuda/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Ajuda ajuda = db.Ajudas.Find(id);
            if (ajuda == null)
            {
                return HttpNotFound();
            }
            return View(ajuda);
        }

        //
        // POST: /Ajuda/Edit/5

        [HttpPost]
        public ActionResult Edit(Ajuda ajuda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ajuda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ajuda);
        }

        //
        // GET: /Ajuda/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Ajuda ajuda = db.Ajudas.Find(id);
            if (ajuda == null)
            {
                return HttpNotFound();
            }
            return View(ajuda);
        }

        //
        // POST: /Ajuda/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ajuda ajuda = db.Ajudas.Find(id);
            db.Ajudas.Remove(ajuda);
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