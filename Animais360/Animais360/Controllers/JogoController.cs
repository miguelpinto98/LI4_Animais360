using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Animais360.Models;
using System.Web.Security;
using System.Web.Routing;

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

        public List<AreaProtegida> getTotalAreas(List<Pais> paises) {
            List<AreaProtegida> aux = new List<AreaProtegida>();
            foreach (Pais p in paises) {
                foreach (AreaProtegida ap in p.AreaProtegidas)
                    aux.Add(ap);
            }
            return aux;
        }

        public ActionResult Play(Jogo jogo) {

            if (Request.IsAuthenticated)
            {
                ViewBag.Pontos = jogo.Pontos;
                ViewBag.NumConts = jogo.Nivel;

                List<int> aux = new List<int>();
                List<AreaProtegida> aps = new List<AreaProtegida>();

                List<Pais> ps = db.Pais.Where(x => x.Continente.ContinenteId == jogo.ContinenteID).ToList();
                int npaises = ps.Count(), r, nareas, idarea;
                Random n = new Random();

                if (npaises > 5)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        r = devolveRandomInt(aux, npaises);
                        Pais p = ps[r];
                        nareas = p.AreaProtegidas.Count();

                        while (nareas == 0)
                        {
                            r = devolveRandomInt(aux, npaises);
                            p = ps[r];
                            nareas = p.AreaProtegidas.Count();
                        }

                        idarea = n.Next(0, nareas);
                        AreaProtegida a = p.AreaProtegidas.ToList()[idarea];
                        aps.Add(a);
                    }
                }
                else
                {
                    List<AreaProtegida> auxAP = getTotalAreas(ps);
                    nareas = auxAP.Count();
                    aux = new List<int>();

                    for (int i = 0; i < 6; i++)
                    {
                        r = n.Next(0, nareas);
                        while (aux.Contains(r))
                        {
                            r = n.Next(0, nareas);
                        }
                        aux.Add(r);
                        aps.Add(auxAP[r]);
                    }
                }
                ViewBag.Areas = aps;
                ViewBag.NomeContinente = db.Continentes.Find(jogo.ContinenteID).ContinenteName;

                return View(jogo);
            }
            else
                return RedirectToAction("Login", "User");
        }

        int devolveIDContinente(string pconts)
        {
            string[] words = pconts.Split('+');
            Random r = new Random();
            int n = r.Next(1, 7);
            while (words.Contains(Convert.ToString(n)))
                n = r.Next(1, 7);

            return n;
        }

        public ActionResult NovoContinente(int id, string pconts) {
            Jogo j = db.Jogos.Find(id);
            j.ContinenteID = devolveIDContinente(pconts);
            j.percorridos = pconts = pconts + "+" + Convert.ToString(j.ContinenteID);

            return RedirectToAction("Play", "Jogo", new RouteValueDictionary(j));
        }

        public ActionResult UpdateJogo(int id, int pontos, int certas, int erradas) {
            Jogo j = db.Jogos.Find(id);
            j.Pontos = pontos;
            j.RespCertas += certas;
            j.RespErradas += erradas;
            j.Nivel++;
            j.DataFim = DateTime.Now;

            db.Entry(j).State = EntityState.Modified;
            db.SaveChanges();

            return Json(j, JsonRequestBehavior.AllowGet);
        }

        int getPontosDificuldade(int dif) {
            if (dif == 1) return 50;
            if (dif == 2) return 300;
            if (dif == 3) return 1000;
            else return -1;
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
                jg.Personagem = "Tarzan";
                jg.RespCertas = 0;
                jg.RespErradas = 0;
                jg.Pontos = getPontosDificuldade(jogo.DificuldadeID);
                jg.DifQualitativa = jogo.DificuldadeID;
                jg.Estado = 0; /* Apagado ou não */
                jg.Sucesso = 0; /* 0 Não Concluído e 1 se concluído */
                jg.ContinenteID = jogo.ContinenteID;
                jg.percorridos = Convert.ToString(jogo.ContinenteID);

                if (id != 0) {
                    User us = db.Users.Find(id);
                    jg.User = us;
                    
                    us.Jogos.Add(jg);
                    jg.User.Jogos.Add(jg);
                    db.SaveChanges();

                    return RedirectToAction("Play", "Jogo", new RouteValueDictionary(jg));
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
            j.RespCertas += certas;
            j.RespErradas += erradas;
            j.Nivel++;

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