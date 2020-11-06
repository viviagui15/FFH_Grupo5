using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FAR_FROM_HOME.EF;
using System.Data;
using System.Data.Entity;
using System.Net;




namespace FAR_FROM_HOME.Controllers
{
    public class FFHController : Controller
    {    
        
        
        // GET: FFH
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Grilla()
        {
            return View();
        }
    }



    }



