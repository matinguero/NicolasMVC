using NicolasMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NicolasMVC.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        [Authorize]
        public ActionResult Index()
        {

            //EVALUO SI TIENE PERMISOS
            //if (!Usuarios.TieneAccesoAdministracion(int.Parse(Session["PERFIL_ID"].ToString()), "Usuarios/Index"))
            //{
            //    return Redirect("/Home/Index");
            //}
            //TODO: evaluar si tiene el perfil de MOTU para poder verlo y si no, redirigirlo a otro lado






            ViewBag.Title = "Página de Listado de Usuarios";
            ViewBag.AlertMessage = "";

            List<Models.Usuarios> ListaUsuarios = new List<Models.Usuarios>();

            string sRet = "";
            Usuarios.ObtenerUsuarios(ref ListaUsuarios, -1, ref sRet);

            CargarPerfiles();


            if (sRet != "")
            {
                ViewBag.AlertMessage = sRet;

            }

            return View(ListaUsuarios);
        }

        public ActionResult FiltrarPorPerfil(int Perfiles2)
        {

            //TODO: evaluar si tiene el perfil de MOTU para poder verlo y si no, redirigirlo a otro lado

            ViewBag.Title = "Página de Listado de Usuarios";
            ViewBag.AlertMessage = "";

            List<Models.Usuarios> ListaUsuarios = new List<Models.Usuarios>();

            string sRet = "";
            Usuarios.ObtenerUsuarios(ref ListaUsuarios, Perfiles2, ref sRet);

            CargarPerfiles();


            if (sRet != "")
            {
                ViewBag.AlertMessage = sRet;
            }

            return View("Index", ListaUsuarios);
            //return View(ListaUsuarios);

        }

        public ActionResult Detalle(int iUsuarioID)
        {
            //TODO: evaluar si tiene el perfil de MOTU para poder verlo y si no, redirigirlo a otro lado

            Usuarios UsuarioDetalle = new Usuarios();

            string sRetError = "";

            if (iUsuarioID != 0)
            {
                //TENGO QUE IR A BUSCAR LOS DATOS DE ESTE USUARIO A LA BASE
                UsuarioDetalle = Usuarios.ObtenerDetalleUsuario(iUsuarioID, ref sRetError);
            }


            CargarPerfilesUsuario();

            ViewBag.AlertMessage = "";

            if (sRetError != "")
            {
                ViewBag.AlertMessage = sRetError;
                return View();
            }
            else
            {
                return View(UsuarioDetalle);
            }


        }
    
        void CargarPerfiles()
        {
            string sRet = "";
            List<Models.PerfilesUsuario> ListaPerfiles = (List<PerfilesUsuario>)PerfilesUsuario.ObtenerPerfiles(ref sRet);

            PerfilesUsuario lstTodosLosPerfiles = new PerfilesUsuario();
            lstTodosLosPerfiles.id = -1;
            lstTodosLosPerfiles.Descripcion = "Todos los perfiles";

            ListaPerfiles.Insert(0, lstTodosLosPerfiles);


            ViewData["PERFILES"] = ListaPerfiles;
        }

        void CargarPerfilesUsuario()
        {
            string sRet = "";
            List<Models.PerfilesUsuario> ListaPerfiles = (List<PerfilesUsuario>)PerfilesUsuario.ObtenerPerfiles(ref sRet);



            ViewData["PERFILES"] = ListaPerfiles;
        }


        [HttpPost]
        public ActionResult GuardarUsuario(Usuarios model)
        {

            //TODO: evaluar si tiene el perfil de MOTU para poder verlo y si no, redirigirlo a otro lado


            if (ModelState.IsValid)
            {
                string sRet = "";

                //ViewBag.Title = "Página de Listado de Usuarios";
                ViewBag.AlertMessage = "Edición de Usuario: " + model.id.ToString() + " / " + model.nombre.ToString();

                //AGREGAR LOGICA SOLAMENTE PARA ACTUALIZAR EL USUARIO ENVIADO

                if (model.id == 0)
                {
                    //INSERTO USUARIO
                    sRet = Usuarios.InsertarUsuario(model);
                }
                else
                {
                    //UPDATEO USUARIO
                    sRet = Usuarios.ModificarUsuario(model);
                }


                if (sRet == "")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    CargarPerfiles();
                    return View("Detalle", model);
                }


            }

            else
            {

                CargarPerfiles();
                return View("Detalle", model);
            }








        }



    }
}