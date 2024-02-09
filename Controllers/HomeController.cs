using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NicolasMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            string sRet = "";
            List<Models.Noticia> ListaNoticias = (List<Models.Noticia>)Models.Noticia.ObtenerNoticias(-1, 1, ref sRet);





            return View(ListaNoticias);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.(ViewBagMessage)";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.(ViewBagMessage)";

            return View();
        }

        

        public ActionResult Formulario(int ?id)
        {
            ViewBag.Mensajito = "Your Form page. with id: "+ id.ToString() + "  (ViewBagMessage)";

            return View();
        }
        [HttpPost]

        public ActionResult GuardarFormulario(string select_saludo)
        {
            ViewBag.Mensajito = "Your Form page. with id: " + select_saludo.ToString() + "  (ViewBagMessage)";

            return View("Formulario");
        }

    }
}