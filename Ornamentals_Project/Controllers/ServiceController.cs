using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ornamentals_Project.Controllers
{
    public class ServiceController : Controller
    {
        private Models.Ornamentals_dbEntities db = new Models.Ornamentals_dbEntities();
        // GET: Service
        public ActionResult Index(string id="servicio")
        {
            var productos = db.Producto
                .Where(x => x.Tipo.Equals(id))
                /*.Take(21)*/
                .ToList();
            return View(productos);
        }

        public ActionResult Service(string id)
        {
            int llave = int.Parse(id);
            var producto = db.Producto
                .Where(x => x.ProductoId == llave).ToList();
            return View(producto);
        }

        public PartialViewResult Servicios(string id = "servicio")
        {
            return PartialView(db.Producto
                .Where(x => x.Tipo.Equals(id))
                //Take(3)
                .ToList());
        }

        [HttpPost]
        public JsonResult RealizarPedidoServicio(List<PedidosDetalle> p)
        {
            // guardar en base de datos
            var clienteid = Helper.SessionHelper.GetUser();
            var pcab = new Models.Pedido
            {
                ClienteId = clienteid,
                Estado = "P",
                Fecha = DateTime.Now
            };

            db.Pedido.Add(pcab);
            db.SaveChanges();


                var pdet = new Models.PedidoDetalle
                {
                    PedidoId = pcab.PedidoId,
                    Cantidad = p.ElementAt(0).Cantidad,
                    ProductoId = p.ElementAt(0).ProductoId,
                    Lugar = p.ElementAt(0).Lugar,
                    FechaEntrega = p.ElementAt(0).FechaEntrega
                };
                db.PedidoDetalle.Add(pdet);
                db.SaveChanges();


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public class PedidosDetalle
        {
            public int ProductoId { get; set; }
            public string Denominacion { get; set; }
            public int Cantidad { get; set; }
            public string Imagen { get; set; }
            public decimal Precio { get; set; }
            public string Lugar { get; set; }
            public string FechaEntrega { get; set; }
        }
    }
}