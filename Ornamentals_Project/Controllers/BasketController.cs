using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ornamentals_Project.Controllers
{
    public class BasketController : Controller
    {
        private Models.Ornamentals_dbEntities bd = new Models.Ornamentals_dbEntities();
        // GET: Basket
        public ActionResult Index(string id = "")
        {
            var productos = bd.Producto
                .Where(x => x.Descripcion.Contains(id))
                .Take(6)
                .ToList();
            return View(productos);
        }
        // GET: Checkout1
        public ActionResult Checkout1()
        {
            var cliente = bd.Cliente.Find(Helper.SessionHelper.GetUser());
            return View(cliente);
        }
        // GET: Checkout2
        public ActionResult Checkout2()
        {
            return View();
        }
        // GET: Checkout3
        public ActionResult Checkout3()
        {
            return View();
        }
        // GET: Checkout4
        public ActionResult Checkout4()
        {
            return View();
        }
        public ActionResult ActualizarCliente([Bind(Include = "ClienteId,Nombres,Apellidos,Correo,Celular,Direccion")] Models.Cliente c)
        {
            
            if (ModelState.IsValid)
            {
                var dato = bd.Cliente.Find(c.ClienteId);
                dato.Nombres = c.Nombres;
                dato.Apellidos = c.Apellidos;
                dato.Correo = c.Correo;
                dato.Celular = c.Celular;
                dato.Direccion = c.Direccion;
                bd.Entry(dato).State = EntityState.Modified;
                //bd.Entry(dato).CurrentValues.SetValues(c);
                //bd.Cliente.Attach(c);
                bd.SaveChanges();
                //Helper.SessionHelper.AddUserToSession(c.ClienteId.ToString());
                return RedirectToAction("Checkout2", "Basket");
            }else
            {
                return RedirectToAction("Checkout1", "Basket");
            }
            
        }

        [HttpPost]
        public JsonResult RealizarPedido(List<Pedidos> p)
        {
            // guardar en base de datos
            var clienteid = Helper.SessionHelper.GetUser();
            var pcab = new Models.Pedido
            {
                ClienteId = clienteid,
                Estado = "P",
                Fecha = DateTime.Now
            };

            bd.Pedido.Add(pcab);
            bd.SaveChanges();


            foreach (var item in p)
            {
                if (ModelState.IsValid)
                {
                    var dato = bd.Producto.Find(item.ProductoId);
                    dato.Existencias = dato.Existencias - item.Cantidad;
                    bd.Entry(dato).State = EntityState.Modified;
                    bd.SaveChanges();
                }
                var pdet = new Models.PedidoDetalle
                {
                    PedidoId = pcab.PedidoId,
                    Cantidad = item.Cantidad,
                    ProductoId = item.ProductoId
                };
                bd.PedidoDetalle.Add(pdet);
                bd.SaveChanges();
            }

            return Json(true);  
        }

        public class Pedidos
        {
            public int ProductoId { get; set; }
            public string Denominacion { get; set; }
            public int Cantidad { get; set; }
            public string Imagen { get; set; }
            public decimal Precio { get; set; }
        }

    }
}