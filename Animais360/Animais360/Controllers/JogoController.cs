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

        int devolveRandomInt(List<int> aux, int npaises) {
            Random n = new Random();
            
            int r = n.Next(0, npaises);
            while (aux.Contains(r))
            {
                r = n.Next(0, npaises);
            }
            aux.Add(r);

            return r;
        }

        List<Pais> getPaisesContinentes(int idc) {
            List<Pais> aux = new List<Pais>();

            foreach(Pais p in db.Pais.Where(x => x.Continente.ContinenteId == idc))
                aux.Add(p);

            return aux;
        }

        public ActionResult Play(int id, int continente, int? pontos) {
            ViewBag.Pontos = pontos;
            Jogo jogo = db.Jogos.Find(id);
            int dif = jogo.DifQualitativa;

            List<int> aux = new List<int>();
            List<AreaProtegida> aps = new List<AreaProtegida>();

            List<Pais> ps = db.Pais.Where(x => x.Continente.ContinenteId == continente).ToList();
            int npaises = ps.Count(), r, nareas, idarea;
            Random n = new Random();
  
            for (int i = 0; i < 6; i++) {
                r = devolveRandomInt(aux, npaises);
                Pais p = ps[r];
                nareas = p.AreaProtegidas.Count();

                while (nareas == 0) {
                    r = devolveRandomInt(aux, npaises);
                    p = ps[r];
                    nareas = p.AreaProtegidas.Count();
                }                

                idarea = n.Next(0, nareas);
                AreaProtegida a = p.AreaProtegidas.ToList()[idarea];
                aps.Add(a);
            }
            ViewBag.Areas = aps;
            
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

                    return RedirectToAction("Play", "Jogo", new { id = jg.JogoId , continente = jogo.ContinenteID, pontos = 0});
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

        public ActionResult GameOver(int id) {
            Jogo j = db.Jogos.Find(id);

            return View(j);
        }

        //POST
        public ActionResult PostGameOver(int id, int pontos, int sucess, int certas, int erradas) {
            Jogo j = db.Jogos.Find(id);

            j.DataFim = DateTime.Now;
            j.Estado = 1; //1 - Concluído 0- A decorrer
            j.Sucesso = sucess;
            j.RespCertas = certas;
            j.RespErradas = erradas;
            j.Nivel = 5;

            if (pontos <= 0)
                j.Pontos = 0;
            else
                j.Pontos = pontos;

            db.Entry(j).State = EntityState.Modified;
            db.SaveChanges();

            return Json(j, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}