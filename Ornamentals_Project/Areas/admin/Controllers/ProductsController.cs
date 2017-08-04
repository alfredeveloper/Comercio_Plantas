using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ornamentals_Project.Models;
using System.IO;

namespace Ornamentals_Project.Areas.admin.Controllers
{
    public class ProductsController : Controller
    {
        private Ornamentals_dbEntities db = new Ornamentals_dbEntities();

        // GET: admin/Products
        public ActionResult Index()
        {
            return View(db.Producto.ToList());
        }

        // GET: admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // GET: admin/Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductoId,Denominacion,Tipo,Descripcion,Precio,Existencias,Activo")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Producto.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductoId,Denominacion,Tipo,Descripcion,Precio,Existencias,Activo")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(producto);
        }

        // GET: admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = db.Producto.Find(id);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }

        // POST: admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.Producto.Find(id);
            db.Producto.Remove(producto);
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


        //codigo de adjuntar
        public JsonResult Adjuntar(int ProductoId, HttpPostedFileBase documento)
        {
            var respuesta = new Models.ResponseModel
            {
                respuesta = true,
                error = ""
            };

            if (documento != null)
            {
                string adjunto = DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(documento.FileName);
                documento.SaveAs(Server.MapPath("~/ImgProductos/" + adjunto));

                db.ProductoImagen.Add(new ProductoImagen { ProductoId = ProductoId, Imagen = adjunto, Titulo = "Ejemplo", Descripcion = "Ejemplo" });
                db.SaveChanges();

            }
            else
            {
                respuesta.respuesta = false;
                respuesta.error = "Debe adjuntar un documento";
            }

            return Json(respuesta);
        }

        public PartialViewResult Adjuntos(int ProductoId)
        {
            return PartialView(db.ProductoImagen.Where(x => x.ProductoId == ProductoId).ToList());
        }

        public JsonResult EliminarImagen(int ProductoImagenId)
        {
            var rpt = new Models.ResponseModel()
            {
                respuesta = true,
                error = ""
            };
            var img = db.ProductoImagen.Find(ProductoImagenId);

            if (System.IO.File.Exists(Server.MapPath("~/ImgProductos/" + img.Imagen)))
                System.IO.File.Delete(Server.MapPath("~/ImgProductos/" + img.Imagen));

            db.ProductoImagen.Remove(img);
            db.SaveChanges();

            return Json(rpt);
        }

        #region Vista Pedidos
        public ActionResult Pedidos()
        {
            var pedidos = db.Pedido.ToList();
            return View(pedidos);
        }
        #endregion

        #region Vista Pedido Detalles
        public ActionResult PedidoDetalles(int id)
        {
            var pedidosDetalle = db.PedidoDetalle
                .Where(x => x.PedidoId.Equals(id))
                .ToList();
            return View(pedidosDetalle);
        }
        #endregion
        #region Vista Producto 
        public ActionResult Producto(int id)
        {
            var producto = db.Producto
                .Where(x => x.ProductoId.Equals(id))
                .ToList();
            return View(producto);
        }
        #endregion

    }
}
