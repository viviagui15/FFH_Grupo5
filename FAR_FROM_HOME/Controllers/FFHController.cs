using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FAR_FROM_HOME.EF; 

namespace FAR_FROM_HOME.Controllers
{
    public class FFHController : Controller
    {
        DATABASE_FFHEntities contexto;
        // GET: Usuario
        public FFHController()
        {
            contexto = new DATABASE_FFHEntities();
        }
        // GET: FFH
        public ActionResult Index()
        {
            List<USUARIODT> usuarios = contexto.USUARIODT.ToList();
            return View(usuarios);
        }

        public ActionResult Grilla()
        {
            return View();
        }
    }
}