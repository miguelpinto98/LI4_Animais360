using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Animais360.Models;
using System.IO;

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
        public ActionResult Create(int id, int tipo, CreateQuestao questao) {
            if (ModelState.IsValid) {

                Questao q = new Questao();
                q.DifQuantitativa = questao.DifQuantitativa;
                q.Pergunta = questao.Pergunta;
                if (tipo == 1) {
                    q.Resposta = questao.RespCorreta1 + ";" + questao.RespCorreta2;
                    q.Hipoteses = questao.Resposta1 + ";" + questao.Resposta2 +";"+questao.Resposta3 + ";" + questao.Resposta4 +";"+questao.Resposta5 + ";" + questao.Resposta6;
                    q.Tipo = tipo;
                } else {
                    if (tipo == 2) {
                        q.Resposta = questao.RespCorreta1;
                        q.Hipoteses = questao.Resposta1 + ";" + questao.Resposta2 + ";" + questao.Resposta3 + ";" + questao.Resposta4;
                        q.Tipo = tipo;
                    } else {
                        if (tipo == 3) {
                            q.Resposta = questao.RespCorreta1;
                            q.Hipoteses = questao.Resposta1 + ";" + questao.Resposta2 + ";" + questao.Resposta3 + ";" + questao.Resposta4;
                            q.Imagem = "/../images/desafios/" + questao.Imagem;
                            q.Tipo = tipo;
                        } else {
                            if (tipo == 4) {
                                q.Resposta = questao.RespCorreta1;
                                q.Hipoteses = questao.Resposta1 + ";" + questao.Resposta2 + ";" + questao.Resposta3 + ";" + questao.Resposta4;
                                
                                q.Tipo = tipo;
                            } else {
                                if (tipo == 5) {
                                    q.Resposta = questao.RespCorreta1;
                                    q.Hipoteses = questao.Resposta1 + ";" + questao.Resposta2 + ";" + questao.Resposta3 + ";" + questao.Resposta4;

                                    q.Tipo = tipo;
                                } else {
                                    return HttpNotFound();
                                }
                            } 
                        }
                    }
                }
                q.AreaProtegida = db.AreaProtegidas.Find(id);
                q.AreaProtegida.Questoes.Add(q);
               
                Ajuda aj = new Ajuda();
                aj.Grau = 1;
                aj.Pista = questao.Ajuda1 +";"+questao.Ajuda2+";"+questao.Ajuda3;
                aj.Questao = q;
                db.Ajudas.Add(aj);
               
                db.Questoes.Add(q);
                db.SaveChanges();
                
                return RedirectToAction("Index", "Questao", new { id = id });
            } 

            return View(questao);
        }

        //
        // GET: /Questao/Edit/5

        public ActionResult Edit(int id = 0, int tipo=0) {
            Questao questao = db.Questoes.Find(id);
            ViewBag.ID = id;

            Ajuda a = db.Ajudas.Where(x => x.Questao.QuestaoID == questao.QuestaoID).First();
            questao.HipAjuda = a.Pista;

            if (questao == null)
            {
                return HttpNotFound();
            }
            return View(questao);
        }

        //
        // POST: /Questao/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Questao questao)
        {
            if (ModelState.IsValid)
            {
                Questao q = db.Questoes.Find(questao.QuestaoID);
                q.DifQuantitativa = questao.DifQuantitativa;
                q.Hipoteses = questao.Hipoteses;
                q.Pergunta = questao.Pergunta;
                q.Resposta = questao.Resposta;

                Ajuda a = db.Ajudas.Where(x => x.Questao.QuestaoID == q.QuestaoID).First();
                a.Pista = questao.HipAjuda;

                db.SaveChanges();
                return RedirectToAction("Index", "Questao", new { id = q.AreaProtegida.AreaProtegidaID });
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
            int x = questao.AreaProtegida.AreaProtegidaID;

            IList<Ajuda> ajs = questao.Ajudas.Where(m => m.Questao.QuestaoID == id).ToList();
            foreach(Ajuda a in ajs) {
                db.Ajudas.Remove(a);
            }
            db.Questoes.Remove(questao);
            db.SaveChanges();
            return RedirectToAction("Index", "Questao", new { id = x});
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}