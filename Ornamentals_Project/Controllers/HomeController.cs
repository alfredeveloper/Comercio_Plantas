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

        public ActionResult Login(string usuario, string clave)
        {
            var u = db.Cliente.FirstOrDefault(x => x.Usuario == usuario && x.Clave == clave);
            if (u != null)
            {
                Helper.SessionHelper.AddUserToSession(u.ClienteId.ToString());
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Helper.SessionHelper.DestroyUserSession();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RegistrarCliente(Models.Cliente c)
        {
            db.Cliente.Add(c);
            db.SaveChanges();
            Helper.SessionHelper.AddUserToSession(c.ClienteId.ToString());
            return RedirectToAction("Index", "Home");
        }

        public static string ObtenerNombreUsuario()
        {
            using (var b = new Models.Ornamentals_dbEntities())
            {
                return b.Cliente.Find(Helper.SessionHelper.GetUser()).Nombres;
            }
        }
        public static string ObtenerPasswordUsuario()
        {
            using (var b = new Models.Ornamentals_dbEntities())
            {
                return b.Cliente.Find(Helper.SessionHelper.GetUser()).Clave;
            }
        }
    }
}