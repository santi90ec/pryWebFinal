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
    public class OrdenComprasController : Controller
    {
        private IngWebEntities db = new IngWebEntities();

        // GET: OrdenCompras
        [Authorize]
        public ActionResult Index()
        {
            var ordenCompra = db.OrdenCompra.Include(o => o.Descuetnos).Include(o => o.Producto);
            return View(ordenCompra.ToList());
        }

        // GET: OrdenCompras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenCompra ordenCompra = db.OrdenCompra.Find(id);
            if (ordenCompra == null)
            {
                return HttpNotFound();
            }
            return View(ordenCompra);
        }

        // GET: OrdenCompras/Create
        public ActionResult Create()
        {
            ViewBag.idDescuento = new SelectList(db.Descuetnos, "idDescuento", "nombreDescuento");
            ViewBag.idProducto = new SelectList(db.Producto, "idProducto", "NombreProduct");
            return View();
        }

        // POST: OrdenCompras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idOrden,cantidadProducto,SubtotalPoducto,idProducto,idDescuento")] OrdenCompra ordenCompra)
        {
            if (ModelState.IsValid)
            {
                db.OrdenCompra.Add(ordenCompra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idDescuento = new SelectList(db.Descuetnos, "idDescuento", "nombreDescuento", ordenCompra.idDescuento);
            ViewBag.idProducto = new SelectList(db.Producto, "idProducto", "NombreProduct", ordenCompra.idProducto);
            return View(ordenCompra);
        }

        // GET: OrdenCompras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenCompra ordenCompra = db.OrdenCompra.Find(id);
            if (ordenCompra == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDescuento = new SelectList(db.Descuetnos, "idDescuento", "nombreDescuento", ordenCompra.idDescuento);
            ViewBag.idProducto = new SelectList(db.Producto, "idProducto", "NombreProduct", ordenCompra.idProducto);
            return View(ordenCompra);
        }

        // POST: OrdenCompras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idOrden,cantidadProducto,SubtotalPoducto,idProducto,idDescuento")] OrdenCompra ordenCompra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenCompra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idDescuento = new SelectList(db.Descuetnos, "idDescuento", "nombreDescuento", ordenCompra.idDescuento);
            ViewBag.idProducto = new SelectList(db.Producto, "idProducto", "NombreProduct", ordenCompra.idProducto);
            return View(ordenCompra);
        }

        // GET: OrdenCompras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrdenCompra ordenCompra = db.OrdenCompra.Find(id);
            if (ordenCompra == null)
            {
                return HttpNotFound();
            }
            return View(ordenCompra);
        }

        // POST: OrdenCompras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrdenCompra ordenCompra = db.OrdenCompra.Find(id);
            db.OrdenCompra.Remove(ordenCompra);
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
