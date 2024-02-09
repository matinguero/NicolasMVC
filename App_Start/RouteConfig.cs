using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NicolasMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
               name: "DetalleNoticiaAdmin",
               url: "Noticias/EditarNoticia/{noticia_id}",
               defaults: new { controller = "Noticias", action = "EditarNoticia" },
               constraints: new { noticia_id = @"\d+" }
           );

            routes.MapRoute(
               name: "DetalleNoticia",
               url: "Noticias/Detalle/{noticia_id}",
               defaults: new { controller = "Noticias", action = "Detalle" },
               constraints: new { noticia_id = @"\d+" }
           );

            routes.MapRoute(
                name: "DetalleUsuario",
                url: "Usuarios/Detalle/{iUsuarioID}",
                defaults: new { controller = "Usuarios", action = "Detalle" },
                constraints: new { iUsuarioID = @"\d+" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
