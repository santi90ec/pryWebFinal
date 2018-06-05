using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pryWeb.Models;

namespace pryWeb.Controllers
{
    public class DescuetnosController : Controller
    {
        private IngWebEntities db = new IngWebEntities();

        // GET: Descuetnos
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Descuetnos.ToList());
        }

        // GET: Descuetnos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Descuetnos descuetnos = db.Descuetnos.Find(id);
            if (descuetnos == null)
            {
                return HttpNotFound();
            }
            return View(descuetnos);
        }

        // GET: Descuetnos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Descuetnos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idDescuento,nombreDescuento,porcentajeDecuento")] Descuetnos descuetnos)
        {
            if (ModelState.IsValid)
            {
                db.Descuetnos.Add(descuetnos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(descuetnos);
        }

        // GET: Descuetnos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Descuetnos descuetnos = db.Descuetnos.Find(id);
            if (descuetnos == null)
            {
                return HttpNotFound();
            }
            return View(descuetnos);
        }

        // POST: Descuetnos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idDescuento,nombreDescuento,porcentajeDecuento")] Descuetnos descuetnos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(descuetnos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(descuetnos);
        }

        // GET: Descuetnos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Descuetnos descuetnos = db.Descuetnos.Find(id);
            if (descuetnos == null)
            {
                return HttpNotFound();
            }
            return View(descuetnos);
        }

        // POST: Descuetnos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Descuetnos descuetnos = db.Descuetnos.Find(id);
            db.Descuetnos.Remove(descuetnos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
