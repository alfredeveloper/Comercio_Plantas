using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ornamentals_Project.Controllers
{
    public class HomeController : Controller
    {
        private Models.Ornamentals_dbEntities db = new Models.Ornamentals_dbEntities();
        // GET: Home
        public ActionResult Index(string id = "")
        {
            var productos = db.Producto
                .Where(x => x.Descripcion.Contains(id))
                .ToList();
            return View(productos);
        }

        // GET: Home
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Faq()
        {
            return View();
        }
        public ActionResult Termcondition()
        {
            return View();
        }
    }
}