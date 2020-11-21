using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FAR_FROM_HOME.EF;
using System.Data;
using System.Data.Entity;
using System.Net;


using System.Web.Security;




namespace FAR_FROM_HOME.Controllers
{
    public class FFHController : Controller
    {
        DATABASE_FFHEntities contexto;
        private DATABASE_FFHEntities db = new DATABASE_FFHEntities();
        // GET: Usuario
        public FFHController()
        {

            contexto = new DATABASE_FFHEntities();
        }

        // GET: FFH
        //public ActionResult Index()
        //{
            
        //    return View();
        //}

        public ActionResult Index()
        {
            
            return View();
            //List<USUARIODT> usuarios = contexto.USUARIODT.ToList();
            //return View(usuarios);
        }







        public ActionResult Grilla()
        {
            return View();
        }
    }



    }



