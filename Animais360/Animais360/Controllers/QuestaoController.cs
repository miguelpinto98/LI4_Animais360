using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Animais360.Models;
using System.IO;
using System.Web.Security;

namespace Animais360.Controllers
{
    public class QuestaoController : Controller
    {
        private Animais360Context db = new Animais360Context();

        //
        // GET: /Questao/

        public ActionResult Index(int id = 0)
        {

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

        public ActionResult Create(int id = 0)
        {

            ViewBag.ID = id;
            return View();
        }

        //
        // POST: /Questao/Create

        [HttpPost]
        public ActionResult Create(int id, int tipo, CreateQuestao questao)
        {
            if (ModelState.IsValid)
            {

                Questao q = new Questao();
                q.DifQuantitativa = questao.DifQuantitativa;
                q.Pergunta = questao.Pergunta;
                if (tipo == 1)
                {
                    q.Resposta = questao.RespCorreta1 + ";" + questao.RespCorreta2;
                    q.Hipoteses = questao.Resposta1 + ";" + questao.Resposta2 + ";" + questao.Resposta3 + ";" + questao.Resposta4 + ";" + questao.Resposta5 + ";" + questao.Resposta6;
                    q.Tipo = tipo;
                }
                else
                {
                    if (tipo == 2)
                    {
                        q.Resposta = questao.RespCorreta1;
                        q.Hipoteses = questao.Resposta1 + ";" + questao.Resposta2 + ";" + questao.Resposta3 + ";" + questao.Resposta4;
                        q.Tipo = tipo;
                    }
                    else
                    {
                        if (tipo == 3)
                        {
                            q.Resposta = questao.RespCorreta1;
                            q.Hipoteses = questao.Resposta1 + ";" + questao.Resposta2 + ";" + questao.Resposta3 + ";" + questao.Resposta4;
                            q.Imagem = "/../images/desafios/" + questao.Imagem;
                            q.Tipo = tipo;
                        }
                        else
                        {
                            if (tipo == 4)
                            {
                                q.Resposta = questao.RespCorreta1;
                                q.Hipoteses = questao.Resposta1 + ";" + questao.Resposta2 + ";" + questao.Resposta3 + ";" + questao.Resposta4;
                                q.Imagem = "/../sounds/desafios/" + questao.Imagem;
                                q.Tipo = tipo;
                            }
                            else
                            {
                                if (tipo == 5)
                                {
                                    q.Resposta = questao.RespCorreta1;
                                    q.Hipoteses = questao.Resposta1 + ";" + questao.Resposta2 + ";" + questao.Resposta3 + ";" + questao.Resposta4;

                                    q.Tipo = tipo;
                                }
                                else
                                {
                                    return HttpNotFound();
                                }
                            }
                        }
                    }
                }
                q.AreaProtegida = db.AreaProtegidas.Find(id);
                q.AreaProtegida.Questoes.Add(q);

                Ajuda aj1 = new Ajuda();
                aj1.Grau = 1;
                aj1.Pista = questao.Ajuda1;
                aj1.Questao = q;
                db.Ajudas.Add(aj1);

                Ajuda aj2 = new Ajuda();
                aj2.Grau = 2;
                aj2.Pista = questao.Ajuda2;
                aj2.Questao = q;
                db.Ajudas.Add(aj2);

                Ajuda aj3 = new Ajuda();
                aj3.Grau = 3;
                aj3.Pista = questao.Ajuda3;
                aj3.Questao = q;
                db.Ajudas.Add(aj3);

                db.Questoes.Add(q);
                db.SaveChanges();

                return RedirectToAction("Index", "Questao", new { id = id });
            }

            return View(questao);
        }

        //
        // GET: /Questao/Edit/5

        public ActionResult Edit(int id = 0, int tipo = 0)
        {
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
            foreach (Ajuda a in ajs)
            {
                db.Ajudas.Remove(a);
            }
            db.Questoes.Remove(questao);
            db.SaveChanges();
            return RedirectToAction("Index", "Questao", new { id = x });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        public ActionResult GetQuestaoArea(int id)
        {
            List<Questao> qarea = db.Questoes.Where(x => x.AreaProtegida.AreaProtegidaID == id).ToList();

            Random n = new Random();
            int np = qarea.Count();
            int r = n.Next(0, np);

            Questao q = qarea[r];

            //if (q.Tipo == 2) 
            string[] words = q.Hipoteses.Split(';');

            CreateQuestao cq = new CreateQuestao();
            cq.id = q.QuestaoID;
            cq.DifQuantitativa = q.DifQuantitativa;
            cq.Pergunta = q.Pergunta;
            cq.RespCorreta1 = q.Resposta;
            cq.Resposta1 = words[0];
            cq.Resposta2 = words[1];
            cq.Resposta3 = words[2];
            cq.Resposta4 = words[3];
            cq.Tipo = q.Tipo;
            cq.Imagem = q.Imagem;

            cq.Ajuda1 = q.Ajudas.Where(x => x.Questao.QuestaoID == cq.id && x.Grau == 1).First().Pista;
            cq.Ajuda2 = q.Ajudas.Where(x => x.Questao.QuestaoID == cq.id && x.Grau == 2).First().Pista;
            cq.Ajuda3 = q.Ajudas.Where(x => x.Questao.QuestaoID == cq.id && x.Grau == 3).First().Pista;

            return Json(cq, JsonRequestBehavior.AllowGet);
        }

        int verificaOpcao(String hipotese, String resposta, int opcao) {
            int flag = 0;
            string[] words = hipotese.Split(';');

            string res = words[opcao - 1];
            if (res.Equals(resposta))
                flag = 1;

            return flag;
        }

        public ActionResult ValidaQuestao(int id, int opcao, int pontos, int idjogo) {
            Questao q = db.Questoes.Find(id);
            AreaProtegida ap = db.AreaProtegidas.Find(q.AreaProtegida.AreaProtegidaID);
            User u = db.Users.Find(Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString()));
            Jogo j = db.Jogos.Find(idjogo);

            int flag = verificaOpcao(q.Hipoteses, q.Resposta, opcao);
            ValidaQuestao vq = new ValidaQuestao();
            vq.res = flag;
            vq.idArea = ap.AreaProtegidaID;

            if (flag==1) { //Acertou
                //Coisas Para a nova tabela
                if (j.DifQualitativa == 1)
                    pontos += 5;
                if (j.DifQualitativa == 2)
                    pontos += 20;
                if (j.DifQualitativa == 3)
                    pontos += 200;

                vq.pontos = pontos;
            } else {    //Errou
                if (j.DifQualitativa == 1)
                    pontos -= 5;
                if (j.DifQualitativa == 2)
                    pontos -= 20;
                if (j.DifQualitativa == 3)
                    pontos -= 200;

                vq.pontos = pontos;
            }

            if (pontos <= 0) {
                vq.gameOver = 1;
            }

            return Json(vq,JsonRequestBehavior.AllowGet);
        }
    }
}