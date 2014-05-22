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
    public class QuestaoController : Controller
    {
        private Animais360Context db = new Animais360Context();

        //
        // GET: /Questao/

        public ActionResult Index(int id=0) {

            if (id == 0)
                HttpNotFound();

            ViewBag.ID = id;

            return View(db.Questoes.Where(de => de.AreaProtegida.AreaProtegidaID == id).ToList());
        }

        //
        // GET: /Questao/Details/5

        public ActionResult Details(int id = 0)
        {
            Questao questao = db.Questoes.Find(id);
            if (questao == null)
            {
                return HttpNotFound();
            }
            return View(questao);
        }

        //
        // GET: /Questao/Create

        public ActionResult Create(int id = 0 ) {

            ViewBag.ID = id;
            return View();
        }

        //
        // POST: /Questao/Create

        [HttpPost]
        public ActionResult Create(Questao questao)
        {
            if (ModelState.IsValid)
            {
                db.Questoes.Add(questao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(questao);
        }

        //
        // GET: /Questao/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Questao questao = db.Questoes.Find(id);
            if (questao == null)
            {
                return HttpNotFound();
            }
            return View(questao);
        }

        //
        // POST: /Questao/Edit/5

        [HttpPost]
        public ActionResult Edit(Questao questao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(questao);
        }

        //
        // GET: /Questao/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Questao questao = db.Questoes.Find(id);
            if (questao == null)
            {
                return HttpNotFound();
            }
            return View(questao);
        }

        //
        // POST: /Questao/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Questao questao = db.Questoes.Find(id);
            db.Questoes.Remove(questao);
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