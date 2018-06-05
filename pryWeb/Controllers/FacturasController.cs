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
    public class FacturasController : Controller
    {
        private IngWebEntities db = new IngWebEntities();

        // GET: Facturas
        [Authorize]
        public ActionResult Index()
        {
            var factura = db.Factura.Include(f => f.Cliente).Include(f => f.Descuetnos).Include(f => f.OrdenCompra);
            return View(factura.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nombreCliente");
            ViewBag.idDescuentoTotal = new SelectList(db.Descuetnos, "idDescuento", "nombreDescuento");
            ViewBag.idOrden = new SelectList(db.OrdenCompra, "idOrden", "idOrden");
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idFactura,fecha,idOrden,idCliente,totalFactura,idDescuentoTotal")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Factura.Add(factura);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nombreCliente", factura.idCliente);
            ViewBag.idDescuentoTotal = new SelectList(db.Descuetnos, "idDescuento", "nombreDescuento", factura.idDescuentoTotal);
            ViewBag.idOrden = new SelectList(db.OrdenCompra, "idOrden", "idOrden", factura.idOrden);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nombreCliente", factura.idCliente);
            ViewBag.idDescuentoTotal = new SelectList(db.Descuetnos, "idDescuento", "nombreDescuento", factura.idDescuentoTotal);
            ViewBag.idOrden = new SelectList(db.OrdenCompra, "idOrden", "idOrden", factura.idOrden);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idFactura,fecha,idOrden,idCliente,totalFactura,idDescuentoTotal")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idCliente = new SelectList(db.Cliente, "idCliente", "nombreCliente", factura.idCliente);
            ViewBag.idDescuentoTotal = new SelectList(db.Descuetnos, "idDescuento", "nombreDescuento", factura.idDescuentoTotal);
            ViewBag.idOrden = new SelectList(db.OrdenCompra, "idOrden", "idOrden", factura.idOrden);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Factura factura = db.Factura.Find(id);
            db.Factura.Remove(factura);
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
