using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Animais360.Models;
using System.Web.Security;

namespace Animais360.Controllers
{
    public class JogoController : Controller
    {
        private Animais360Context db = new Animais360Context();

        //
        // GET: /Jogo/

        public ActionResult Index() {
            ViewBag.IdUser = Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString());
            return View(db.Jogos.ToList());
        }

        //
        // GET: /Jogo/Details/5

        public ActionResult Details(int id = 0)
        {
            Jogo jogo = db.Jogos.Find(id);
            if (jogo == null)
            {
                return HttpNotFound();
            }
            return View(jogo);
        }

        //
        // GET: /Jogo/Create

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Play(int id, int? continente) {
            Jogo jogo = db.Jogos.Find(id);

            ViewBag.ContinenteID = continente;
            ViewBag.Continentes = db.Continentes.ToList();
            //ViewBag.Paises = db.Pais.Count();
            ViewBag.Areas = db.AreaProtegidas.ToList();
            
            return View(jogo);
        }

        //
        // POST: /Jogo/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                Jogo jg = new Jogo();
                jg.DataInicio = DateTime.Now;
                jg.DataFim = DateTime.Now;
                jg.Nivel = 0;
                jg.Personagem = "LINDO";
                jg.RespCertas = 0;
                jg.RespErradas = 0;
                jg.Pontos = 0;
                jg.DifQualitativa = jogo.DificuldadeID;
                jg.Estado = 0; /* Apagado ou não */
                jg.Sucesso = 0; /* 0 Não Concluído e 1 se concluído */

                if (id != 0) {
                    User us = db.Users.Find(id);
                    jg.User = us;
                    
                    //db.Jogos.Add(jg);
                    us.Jogos.Add(jg);
                    jg.User.Jogos.Add(jg);
                    db.SaveChanges();

                    return RedirectToAction("Play", "Jogo", new { id = jg.JogoId , continente = jogo.ContinenteID });
                }
                return HttpNotFound();   
            }

            return View(jogo);
        }

        //
        // GET: /Jogo/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Jogo jogo = db.Jogos.Find(id);
            if (jogo == null)
            {
                return HttpNotFound();
            }
            return View(jogo);
        }

        //
        // POST: /Jogo/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Jogo jogo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jogo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jogo);
        }

        //
        // GET: /Jogo/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Jogo jogo = db.Jogos.Find(id);
            if (jogo == null)
            {
                return HttpNotFound();
            }
            return View(jogo);
        }

        //
        // POST: /Jogo/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Jogo jogo = db.Jogos.Find(id);
            db.Jogos.Remove(jogo);
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