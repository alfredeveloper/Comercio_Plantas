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
            var productos = db.Producto
                .Where(x=>x.Descripcion.Contains(id))
                .Take(9)
                .ToList();
            return View(productos);
        }

        // GET: Store
        public ActionResult Product(string id)
        {
            ViewBag.producto = id;
            return View();
        }
    }
}