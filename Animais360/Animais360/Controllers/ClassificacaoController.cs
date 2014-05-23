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
    public class ClassificacaoController : Controller
    {
        private Animais360Context db = new Animais360Context();

        //
        // GET: /Classificacao/

        public ActionResult Index()
        {
            return View(db.Classificacoes.ToList());
        }

        //
        // GET: /Classificacao/Details/5

        public ActionResult Details(int id = 0)
        {
            Classificacao classificacao = db.Classificacoes.Find(id);
            if (classificacao == null)
            {
                return HttpNotFound();
            }
            return View(classificacao);
        }

        //
        // GET: /Classificacao/Create

        public ActionResult Create() {
            return View();
        }

        //
        // POST: /Classificacao/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Classificacao classificacao)
        {
            if (ModelState.IsValid)
            {
                db.Classificacoes.Add(classificacao);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(classificacao);
        }

        //
        // GET: /Classificacao/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Classificacao classificacao = db.Classificacoes.Find(id);
            if (classificacao == null)
            {
                return HttpNotFound();
            }
            return View(classificacao);
        }

        //
        // POST: /Classificacao/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Classificacao classificacao)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classificacao).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classificacao);
        }

        //
        // GET: /Classificacao/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Classificacao classificacao = db.Classificacoes.Find(id);
            if (classificacao == null)
            {
                return HttpNotFound();
            }
            return View(classificacao);
        }

        //
        // POST: /Classificacao/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Classificacao classificacao = db.Classificacoes.Find(id);
            db.Classificacoes.Remove(classificacao);
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