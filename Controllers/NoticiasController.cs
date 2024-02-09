using NicolasMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NicolasMVC.Controllers
{
    public class NoticiasController : Controller
    {
        // GET: Noticias
        public ActionResult Index()
        {

            string sRet = "";
            List<Models.Noticia> ListaNoticias = (List<Models.Noticia>)Models.Noticia.ObtenerNoticias(-1, 1, ref sRet);


            CargaCategoriasNoticia();


            return View(ListaNoticias);
        }


        public ActionResult Detalle(int noticia_id)
        {

            string sRet = "";
            Models.Noticia DetalleNoticia = (Models.Noticia)Models.Noticia.ObtenerNoticia(noticia_id, ref sRet);

            //ME FIJO SI TIENE DATOS EL CAMPO EN BYTES
            ////if (DetalleNoticia.Foto2 != null)
            ////{
            ////    //Convierto el array de bytes en (base64) para mostrarlo en la vista.
            ////    string FotoEnbase64String = Convert.ToBase64String(DetalleNoticia.Foto2);

            ////    //Paso el base64String a la vista a través del ViewBag.
            ////    ViewBag.FotoBase64 = FotoEnbase64String;
            ////}





            return View(DetalleNoticia);
        }


        [Authorize]
        public ActionResult AdminNoticias()
        {
            string sRet = "";
            List<Models.Noticia> ListaNoticias = (List<Models.Noticia>)Models.Noticia.ObtenerNoticias(-1, -1, ref sRet);

            return View(ListaNoticias);

        }



        [Authorize]
        public ActionResult EditarNoticia(int noticia_id)
        {


            Noticia DetalleNoticia = new Noticia();
            string sRet = "";


            if (noticia_id != 0)
            {
                //TENGO QUE IR A BUSCAR LOS DATOS DE ESTA NOTICIA A LA BASE
                DetalleNoticia = (Models.Noticia)Models.Noticia.ObtenerNoticia(noticia_id, ref sRet);
            }


            ////ME FIJO SI TIENE DATOS EL CAMPO EN BYTES
            //if (DetalleNoticia.Foto2 != null)
            //{
            //    //Convierto el array de bytes en (base64) para mostrarlo en la vista.
            //    string FotoEnbase64String = Convert.ToBase64String(DetalleNoticia.Foto2);

            //    //Paso el base64String a la vista a través del ViewBag.
            //    ViewBag.FotoBase64 = FotoEnbase64String;
            //}

            ////ME FIJO SI TIENE DATOS EL CAMPO EN BYTES
            //if (DetalleNoticia.Foto != null)
            //{
            //    //Paso el base64String a la vista a través del ViewBag.
            //    ViewBag.Foto = "/uploads/noticias/" + DetalleNoticia.Foto;
            //}





            CargaCategoriasNoticia();



            return View(DetalleNoticia);
        }


        [Authorize]
        [HttpPost]
        public ActionResult GuardarNoticia(Models.Noticia noticia)
        {


            if (ModelState.IsValid)
            {
                HttpPostedFileBase foto = Request.Files["fotoupload"];

                string sRet = "";


                //GUARDO LA FOTO EN EL SISTEMA DE ARCHIVOS
                if (foto != null && foto.ContentLength > 0)
                {
                    string sNombreArchivo = System.Guid.NewGuid().ToString() + ".jpg";

                    sRet = SubirFoto(foto, sNombreArchivo);

                    if (sRet == "")
                    {
                        //GUARDO LA NOTICIA EN LA BASE
                        //NECESITO SABER COMO SE LLAMA LA FOTO

                        noticia.Foto = sNombreArchivo;

                    }




                    //GUARDO FOTO2 EN LA BASE
                    //METODO POR ARRAY DE BYTES
                    // Leer la foto como array de bytes.
                    byte[] bytes;
                    using (BinaryReader br = new BinaryReader(foto.InputStream))
                    {
                        bytes = br.ReadBytes(foto.ContentLength);
                    }

                    // Asignar los datos de la foto al modelo de noticia.
                    noticia.Foto2 = bytes;





                }
                else
                {
                    //TODO: ver el stored procedure para revisar si viene con algo o no
                    noticia.Foto2 = null;
                    noticia.Foto = Request.Form["imagen1"];
                }



                //SI EL CHECKBOX NO ESTA CHEQUEADO, NO VIAJA EN EL POST, VIENE NULL
                //SI NO VIENE NULL ES QUE ESTA CHEQUEADO
                if (Request.Form["chkActivo"] is null)
                {
                    noticia.iActivo = 0;
                }
                else
                {
                    noticia.iActivo = 1;
                }





                if (noticia.id == 0)
                {
                    //SI NO ME SUBEN FOTO? LOS DEJO?
                    //INSERTO NOTICIA
                    sRet = Noticia.InsertarNoticia(noticia);
                }
                else
                {
                    //UPDATEO NOTICIA
                    sRet = Noticia.ModificarNoticia(noticia);
                }

            }





            return RedirectToAction("AdminNoticias", "Noticias");
        }





        string SubirFoto(HttpPostedFileBase oFoto, string sNombreArchivo)
        {

            string sRet = "";

            try
            {
                // Obtener el nombre del archivo.
                //string fileName = Path.GetFileName(oFoto.FileName);



                // Definir la ruta de almacenamiento en el sistema de archivos.
                string filePath = Path.Combine(Server.MapPath("~/uploads/noticias"), sNombreArchivo);

                // Guardar la foto en el sistema de archivos.
                oFoto.SaveAs(filePath);

                ViewBag.Foto = filePath;
            }
            catch (Exception ex)
            {
                sRet = ex.Message.ToString();

            }

            return sRet;
        }




        void CargaCategoriasNoticia()
        {
            string sRet = "";
            List<Models.CategoriasNoticia> ListaCategorias = (List<CategoriasNoticia>)CategoriasNoticia.ObtenerCategoriasNoticias(ref sRet);



            ViewData["CATEGORIAS"] = ListaCategorias;
        }

        public ActionResult FiltrarPorCategoria(int Categorias2)
        {

            //TODO: evaluar si tiene el perfil de MOTU para poder verlo y si no, redirigirlo a otro lado

            ViewBag.Title = "Página de Listado de Noticias";
            ViewBag.AlertMessage = "";

            List<Models.Noticia> ListaNoticias = new List<Models.Noticia>();

            string sRet = "";
            Noticia.ObtenerNoticias(ref ListaNoticias, Categorias2, 1, ref sRet);






            CargaCategoriasNoticia();




            if (sRet != "")
            {
                ViewBag.AlertMessage = sRet;
            }

            return View("Index", ListaNoticias);
            //return View(ListaUsuarios);

        }
    }
}