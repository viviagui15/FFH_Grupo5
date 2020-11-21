using FAR_FROM_HOME.EF;
using FAR_FROM_HOME.EF.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;


namespace FAR_FROM_HOME.Controllers
{
    public class PublicacionController : Controller
    {
        DATABASE_FFHEntities contexto;
        private DATABASE_FFHEntities db = new DATABASE_FFHEntities();


        public PublicacionController()
        {
            contexto = new DATABASE_FFHEntities();
        }

        public ActionResult Index(string searching, string estadoPer, string tipo, string datos)
        {
            List<PublicacionesViewModel> lst = null; 
            using (EF.DATABASE_FFHEntities db = new EF.DATABASE_FFHEntities())
            {
                lst = (from d in db.PUBLICACIONDT
                     select new PublicacionesViewModel
                     {
                         //Id=d.ID_PUBLICACION, 
                         ubicacion=d.UBICACION
                     }).ToList(); 

            }
            List<SelectListItem> items = lst.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.ubicacion.ToString(),
                    //Value = d.Id.ToString(),
                    Selected = false
                };

            }); 
            ViewBag.items = items; 

            if (!string.IsNullOrEmpty(searching) || !string.IsNullOrEmpty(estadoPer) || !string.IsNullOrEmpty(tipo) || !string.IsNullOrEmpty(datos))
            {
                if (!string.IsNullOrEmpty(searching)){ 
                return View(db.PUBLICACIONDT.Where(x => x.DESCRIPCION.Contains(searching)));
                } else 
                if (estadoPer == "perdido")
                {
                    return View(db.PUBLICACIONDT.Where(x => x.EST_ENCPERD == "P"));
                } else { return View(db.PUBLICACIONDT.Where(x => x.EST_ENCPERD == "E")); }
            } else {
                var pUBLICACIONDT = db.PUBLICACIONDT.Include(p => p.USUARIODT);
                return View(pUBLICACIONDT.ToList());
            }

   
        }



        public List<PUBLICACIONDT> BuscarPubli ()
        {
            var result = from datos in contexto.PUBLICACIONDT select datos;
            return result.ToList(); 
        } 
        public ActionResult Listaubicaciones ()
        {
            List<PUBLICACIONDT> listazonas = db.PUBLICACIONDT.ToList(); 
            ViewBag.Publicaciones = new SelectList(listazonas,"ubicacion");
            return View(); 
        }

        // GET: Publicacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLICACIONDT pUBLICACIONDT = db.PUBLICACIONDT.Find(id);
            if (pUBLICACIONDT == null)
            {
                return HttpNotFound();
            }
            return View(pUBLICACIONDT);
        }

        // GET: Publicacion/Create
        public ActionResult Create()
        {
            ViewBag.ID_USUARIO = new SelectList(db.USUARIODT, "ID_USUARIO", "ID_USUARIO");
            return View();
        }

        // POST: Publicacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PUBLICACION,ID_USUARIO,UBICACION,RAZA,ESTADO_SALUD,TIPO_MASCOTA,COLOR_MASCOTA,TAMAÑO,EDAD,REDES,SEXO,TRANSITO,EST_ENCPERD,F_PUBLICACION,IMAGEN,DESCRIPCION")] PUBLICACIONDT pUBLICACIONDT)
        {
            HttpPostedFileBase FileBase = Request.Files[0];  //representa un objeto y permite mostrar y administrar el archivo

            if (FileBase.ContentLength == 0)
            {
                ModelState.AddModelError("Imagen", "Agregue la imagen");
            }
            else
            {
                if (FileBase.FileName.EndsWith(".jpg"))
                {
                    //Request.file Permite al servidor asp.net leer los valores del http
                    WebImage image = new WebImage(FileBase.InputStream); //permite la lectura del archivo 

                    pUBLICACIONDT.IMAGEN = image.GetBytes(); //obtenenos la imagen como una matriz de bytes
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Verificar que la imagen sea JPG");
                }

            }



            if (ModelState.IsValid)
            {
                db.PUBLICACIONDT.Add(pUBLICACIONDT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_USUARIO = new SelectList(db.USUARIODT, "ID_USUARIO", "ID_USUARIO", pUBLICACIONDT.ID_USUARIO);
            return View(pUBLICACIONDT);
        }

        // GET: Publicacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLICACIONDT pUBLICACIONDT = db.PUBLICACIONDT.Find(id);
            if (pUBLICACIONDT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_USUARIO = new SelectList(db.USUARIODT, "ID_USUARIO", "ID_USUARIO", pUBLICACIONDT.ID_USUARIO);
            return View(pUBLICACIONDT);
        }

        // POST: Publicacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PUBLICACION,ID_USUARIO,UBICACION,RAZA,ESTADO_SALUD,TIPO_MASCOTA,COLOR_MASCOTA,TAMAÑO,EDAD,REDES,SEXO,TRANSITO,EST_ENCPERD,F_PUBLICACION,IMAGEN,DESCRIPCION")] PUBLICACIONDT pUBLICACIONDT)
        {
            //byte[] imagenActual = null;

            PUBLICACIONDT _pUBLICACIONDT = new PUBLICACIONDT();
            HttpPostedFileBase FileBase = Request.Files[0];  //representa un objeto y permite mostrar y administrar el archivo

            if (FileBase.ContentLength == 0)
            {
                _pUBLICACIONDT = db.PUBLICACIONDT.Find(pUBLICACIONDT.ID_PUBLICACION);
                pUBLICACIONDT.IMAGEN = _pUBLICACIONDT.IMAGEN;
            }
            else
            {


                if (FileBase.FileName.EndsWith(".jpg"))
                {
                    //Request.file Permite al servidor asp.net leer los valores del http
                    WebImage image = new WebImage(FileBase.InputStream); //permite la lectura del archivo 

                    pUBLICACIONDT.IMAGEN = image.GetBytes(); //obtenenos la imagen como una matriz de bytes
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Verificar que la imagen sea JPG");
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(_pUBLICACIONDT).State = EntityState.Detached; //requiere este otro id
                db.Entry(pUBLICACIONDT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_USUARIO = new SelectList(db.USUARIODT, "ID_USUARIO", "ID_USUARIO", pUBLICACIONDT.ID_USUARIO);
            return View(pUBLICACIONDT);
        }








        public ActionResult ListaImagenes()
        {

            var pUBLICACIONDT = db.PUBLICACIONDT.Include(p => p.USUARIODT);
            return View(pUBLICACIONDT.ToList());
        }


        // POST: Publicacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ListaImagenes([Bind(Include = "ID_PUBLICACION,ID_USUARIO,UBICACION,RAZA,ESTADO_SALUD,TIPO_MASCOTA,COLOR_MASCOTA,TAMAÑO,EDAD,REDES,SEXO,TRANSITO,EST_ENCPERD,F_PUBLICACION,IMAGEN,DESCRIPCION")] PUBLICACIONDT pUBLICACIONDT)
        //{
        //    //byte[] imagenActual = null;

        //    PUBLICACIONDT _pUBLICACIONDT = new PUBLICACIONDT();
        //    HttpPostedFileBase FileBase = Request.Files[0];  //representa un objeto y permite mostrar y administrar el archivo

        //    if (FileBase.ContentLength == 0)
        //    {
        //        _pUBLICACIONDT = db.PUBLICACIONDT.Find(pUBLICACIONDT.ID_PUBLICACION);
        //        pUBLICACIONDT.IMAGEN = _pUBLICACIONDT.IMAGEN;
        //    }
        //    else
        //    {


        //        if (FileBase.FileName.EndsWith(".jpg"))
        //        {
        //            //Request.file Permite al servidor asp.net leer los valores del http
        //            WebImage image = new WebImage(FileBase.InputStream); //permite la lectura del archivo 

        //            pUBLICACIONDT.IMAGEN = image.GetBytes(); //obtenenos la imagen como una matriz de bytes
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("Imagen", "Verificar que la imagen sea JPG");
        //        }
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(_pUBLICACIONDT).State = EntityState.Detached; //requiere este otro id
        //        db.Entry(pUBLICACIONDT).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ID_USUARIO = new SelectList(db.USUARIODT, "ID_USUARIO", "ID_USUARIO", pUBLICACIONDT.ID_USUARIO);
        //    return View(pUBLICACIONDT);
        //}













        // GET: Publicacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PUBLICACIONDT pUBLICACIONDT = db.PUBLICACIONDT.Find(id);
            if (pUBLICACIONDT == null)
            {
                return HttpNotFound();
            }
            return View(pUBLICACIONDT);
        }

        // POST: Publicacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PUBLICACIONDT pUBLICACIONDT = db.PUBLICACIONDT.Find(id);
            db.PUBLICACIONDT.Remove(pUBLICACIONDT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult getImage(int id)
        {
            PUBLICACIONDT publi = db.PUBLICACIONDT.Find(id);
            byte[] byteImage = publi.IMAGEN; //aca tenemos la imagen
            MemoryStream memoryStream = new MemoryStream(byteImage); //almacenamos en memoria, crea una secuencia
            Image image = Image.FromStream(memoryStream); //Image clase abstracta , fromstream crea una imagen a partir de un flujo de datos especificado

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg); //cargamos la imagen con formato
            memoryStream.Position = 0; //para que lo tome de manera inmediata
            return File(memoryStream, "image/jpg"); //retorna la imagen
        }


        ////prueba para perdido encontrado 
        //public ActionResult getPE(Boolean id)
        //{

        //    IQueryable<PUBLICACIONDT> lst = from d in db.PUBLICACIONDT
        //                                    select d;


        //    if (id == true)
        //        lst = lst.Where(d => d.ID_PUBLICACION == 1);
        //    foreach (var oElement in lst)
        //    {
        //        //Console.WriteLine(oElement.DESCRIPCION);

        //    }


        //    return View(lst.ToList());
        //}



    }
}



