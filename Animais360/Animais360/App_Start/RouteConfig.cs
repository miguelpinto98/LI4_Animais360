using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Animais360
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "GameOverJogo",
                url: "Jogo/PostGameOver/{id}/{pontos}/{sucess}"
            );

            routes.MapRoute(
                name: "CreateQuestao",
                url: "Questao/{action}/{id}/{tipo}",
                defaults: new { controller = "Questao", action = "Create", tipo = UrlParameter.Optional }
            );
            /*
            routes.MapRoute(
                name: "PlayJogo",
                url: "Jogo/{action}/{id}/{continente}/{pontos}",
                defaults: new { controller = "Jogo", action = "Play", pontos = UrlParameter.Optional }
            );
            */
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}