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
    public class AreaProtegidaController : Controller
    {
        private Animais360Context db = new Animais360Context();

        //
        // GET: /AreaProtegida/

        public ActionResult Index() {
            int id = Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString());
            User us = db.Users.Find(id);
            ViewBag.Tipo = us.Tipo;

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

            int idx = Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString());
            User us = db.Users.Find(idx);
            ViewBag.Tipo = us.Tipo;

            return View();
        }

        //
        // POST: /AreaProtegida/Create

        [HttpPost]
        public ActionResult Create(AreaProtegida ap)
        {
            if (ModelState.IsValid) {
                AreaProtegida area = new AreaProtegida();
                area.AreaNome = ap.AreaNome;
                area.Descricao = ap.Descricao;
                area.Latitude = ap.Latitude;
                area.Longitude = ap.Longitude;
                area.Permitida = 0;

                if (ap.IdContinente != 0 && !ap.NomePais.Equals("")) {
                    if(ap.IdPais==0) {
                        Pais p = new Pais();
                        p.PaisNome = ap.NomePais;
                        p.Continente = db.Continentes.Find(ap.IdContinente);
                        db.Pais.Add(p);
                        area.Pais = p;
                    } else {
                        return View(ap);
                    }
                } else {
                    area.Pais = db.Pais.Find(ap.IdPais);
                }
                

                db.AreaProtegidas.Add(area);
                area.Pais.AreaProtegidas.Add(area);
                db.SaveChanges();
                return RedirectToAction("Index","AreaProtegida");
            }
            return View(ap);
        }

        //
        // GET: /AreaProtegida/Edit/5

        public ActionResult Edit(int id = 0) {
            AreaProtegida areaprotegida = db.AreaProtegidas.Find(id);
            ViewBag.Continentes = db.Continentes.ToList();
            ViewBag.Paises = db.Pais.ToList();
            
            if (areaprotegida == null)
            {
                return HttpNotFound();
            }

            int idx = Convert.ToInt32(Membership.GetUser().ProviderUserKey.ToString());
            User us = db.Users.Find(idx);
            ViewBag.Tipo = us.Tipo;

            return View(areaprotegida);
        }

        //
        // POST: /AreaProtegida/Edit/5

        [HttpPost]
        public ActionResult Edit(AreaProtegida areaprotegida)
        {
            if (ModelState.IsValid) {
                AreaProtegida ap = db.AreaProtegidas.Find(areaprotegida.AreaProtegidaID);


                ap.AreaNome = areaprotegida.AreaNome;
                ap.Latitude = areaprotegida.Latitude;
                ap.Longitude = areaprotegida.Longitude;
                ap.Descricao = areaprotegida.Descricao;

                if (areaprotegida.IdContinente != 0 && !areaprotegida.NomePais.Equals("")) {
                    if (areaprotegida.IdPais == ap.Pais.PaisID) {
                        Pais p = new Pais();
                        p.PaisNome = ap.NomePais;
                        p.Continente = db.Continentes.Find(ap.IdContinente);
                        db.Pais.Add(p);
                        ap.Pais = p;
                        p.AreaProtegidas.Add(ap);
                    } else {
                        return View(areaprotegida);   
                    }
                } else {
                    ap.Pais = db.Pais.Find(ap.IdPais);
                }

                db.Entry(ap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(areaprotegida);
        }

        //
        // GET: /AreaProtegida/Delete/5

        public ActionResult Permission(int id = 0)
        {
            AreaProtegida areaprotegida = db.AreaProtegidas.Find(id);

            if (areaprotegida == null) {
                return HttpNotFound();
            }

            if( areaprotegida.Permitida == 0) {
                areaprotegida.Permitida = 1;
            } else {
                areaprotegida.Permitida = 0;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "AreaProtegida", new { id = areaprotegida.AreaProtegidaID });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}