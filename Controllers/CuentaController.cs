using NicolasMVC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NicolasMVC.Controllers
{
    public class CuentaController : Controller
    {
        // GET: Cuenta
       
        public ActionResult Login()
        {
            Cuenta cuenta = new Cuenta();


            return View(cuenta);

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            return Redirect("~/Home/Index");
        }

        [HttpPost]
        public ActionResult LoginForm(Cuenta model)
        {


            if (ModelState.IsValid)
            {


                string sRet = "";
                DataTable dt = new DataTable();

                sRet = Cuenta.LoginUsuario(model.Usuario.ToString().Trim(), model.Clave.ToString().Trim(), ref dt);

                if (sRet != "")
                {
                    ViewBag.AlertMessage = "Error en el login - " + sRet;
                    ViewBag.Message = "Error en el login - " + sRet;
                    return View("Login", model);
                }


                if (dt.Rows.Count == 1)
                {
                    var authenticationTicket = new FormsAuthenticationTicket(
                1, // Versión de la cookie de autenticación
                model.Usuario, // Nombre del usuario (puedes dejarlo en blanco si lo configurarás después)
                DateTime.Now, // Fecha y hora de inicio de la cookie de autenticación
                DateTime.Now.AddMinutes(30), // Fecha y hora de expiración de la cookie de autenticación
                false // Si la cookie persiste en el disco después de cerrar el navegador (false en este ejemplo)
                , "roles, custom data" // Datos adicionales del usuario (roles, información personalizada, etc.)
            );

                    // Encriptar la cookie de autenticación
                    string encryptedTicket = FormsAuthentication.Encrypt(authenticationTicket);

                    // Crear una cookie de autenticación y agregarla a la respuesta
                    var authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    HttpContext.Response.Cookies.Add(authenticationCookie);

                    Session["ID"] = dt.Rows[0]["id"];
                    Session["NOMBRE"] = dt.Rows[0]["nombre"];
                    Session["PERFIL"] = dt.Rows[0]["perfil"];
                    Session["PERFIL_ID"] = dt.Rows[0]["perfil_id"];

                    //PASO SIGUIENTE INSTANCIAR EL OBJETO USUARIO/CUENTA, LLENARLO Y ESE OBJETO GUARDARLO EN SESSION


                    return Redirect("~/Home/Index");
                }
                else
                {
                    ViewBag.AlertMessage = "Error en el login";
                    ViewBag.Message = "Error en el login";
                    return View("Login", model);
                }




            }
            else
            {
                return View("Login", model);
            }





        }
    }
}
