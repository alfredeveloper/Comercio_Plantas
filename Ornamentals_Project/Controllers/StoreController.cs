using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ornamentals_Project.Controllers
{
    public class StoreController : Controller
    {
        private Models.Ornamentals_dbEntities db = new Models.Ornamentals_dbEntities();
        // GET: Store
        public ActionResult Index(string id = "")
        {
            

            if (id == "precio")
            {
                var productos = db.Producto.OrderBy(x => x.Precio)
                .Take(21)
                .ToList();
                return View(productos);
            }
            else if (id == "nombre")
            {
                var productos = db.Producto.OrderBy(x => x.Denominacion)
                .Take(21)
                .ToList();
                return View(productos);
            }
            else
            {
                var productos = db.Producto
                .Where(x => x.Descripcion.Contains(id))
                .Take(21)
                .ToList();
                return View(productos);
            }
            //return View(productos);
        }

        // GET: Store
        public ActionResult Product(string id)
        {
            int llave = int.Parse(id);
            var producto = db.Producto
                .Where(x => x.ProductoId == llave).ToList();
            return View(producto);
        }

        public PartialViewResult ProductosPrecios(string id="")
        {
            return PartialView(db.Producto
                .Where(x => x.Descripcion.Contains(id))
                .Take(3)
                .ToList());
        }

        public PartialViewResult ProductosDestacado(string id = "")
        {
            
            return PartialView(db.Producto
                .Where(x => x.Descripcion.Contains(id))
                .Take(3)
                .ToList());
        }
        /*
        public ActionResult Category(string id = )
        {
           
            return View(producto);
        }*/
    }
}