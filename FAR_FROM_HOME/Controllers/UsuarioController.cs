using FAR_FROM_HOME.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FAR_FROM_HOME.Controllers
{
    public class UsuarioController : Controller
    {

        DATABASE_FFHEntities contexto;
        private DATABASE_FFHEntities db = new DATABASE_FFHEntities();
    
        // GET: Usuario
        public UsuarioController()
        {
            contexto = new DATABASE_FFHEntities();
        }

        //GET: Usuario
        public ActionResult Logueo(string message = "")
        {
           


            ViewBag.Message = message;
            return Redirect("/Publicacion/Index"); 
        }


        [HttpPost]
        public ActionResult Login(string email, string password)
        {

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {

                var user = db.USUARIODT.FirstOrDefault(e => e.MAIL == email && e.CONTRASENIA == password);
                if (user != null)
                {
                    //se encontro usuario
                    FormsAuthentication.SetAuthCookie(user.MAIL, true);
                    return RedirectToAction("Logueo", "Usuario"); //antes estaba Profile

                }
                else
                {
                    return RedirectToAction("Logueo", new { message = "No encontramos tus datos" });
                }
            }
            else
            {
                return RedirectToAction("Logueo", new { message = "Llena los campos" });

            }
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Logueo", "Usuario");

        }


        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAIL,NOMBRE,APELLIDO,CONTRASENIA")] USUARIODT USUARIO)
        {
            if (ModelState.IsValid)
            {
                db.USUARIODT.Add(USUARIO);
                db.SaveChanges();
                return Redirect("/Publicacion/Index");
            }

            return View(USUARIO);
        }


        public ActionResult ErrorLogueo()
        {
            return View();
        }

    

        public ActionResult UsuarioLogueado()
        {
            return View();
        }


        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
