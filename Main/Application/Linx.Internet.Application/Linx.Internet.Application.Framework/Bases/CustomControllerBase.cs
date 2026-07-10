// -----------------------------------------------------------------------
// <copyright file="CustomControllerBase.cs" company="Linx Sistemas">
// Copyright (c) Linx Sistemas. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
namespace Linx.Internet.Application.Framework.Bases
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// Classe CustomControllerBase  Herada caracteristicas de Controller
    /// </summary>
    public abstract class CustomControllerBase : Controller
    {
        /// <summary>
        /// Metodo ExecuteCore
        /// </summary>
        protected override void ExecuteCore()
        {
            var langHeader = string.Empty;
            string culturaPadrao = ConfigurationManager.AppSettings["Cultura.Padrao"].ToLower();
            string listaSuportada = ConfigurationManager.AppSettings["Cultura.ListaSuportada"].ToLower();
            bool porCookieHabilitado = bool.Parse(ConfigurationManager.AppSettings["Cultura.PorCookie"]);
            bool porURLHabilitado = bool.Parse(ConfigurationManager.AppSettings["Cultura.PorURL"]);
            bool porBrowserHabilitado = bool.Parse(ConfigurationManager.AppSettings["Cultura.PorBrowser"]);

            if (porCookieHabilitado)
            {
                //// load the culture info from the cookie
                var cookie = HttpContext.Request.Cookies["LinxB2C.CurrentUICulture"];
                if (cookie != null)
                {
                    //// set the culture by the cookie content
                    langHeader = cookie.Value;
                }
            }

            if (porURLHabilitado && langHeader.Length == 0)
            {
                //// verifica se cultura veio por URL
                if (RouteData.Values["lang"] != null && !string.IsNullOrWhiteSpace(RouteData.Values["lang"].ToString()))
                {
                    //// set the culture from the route data (url)
                    langHeader = RouteData.Values["lang"].ToString();
                }
            }

            if (porBrowserHabilitado && langHeader.Length == 0)
            {
                //// verifica se cultura veio pelo BROWSER
                if (HttpContext.Request.UserLanguages.Count() > 0)
                {
                    langHeader = HttpContext.Request.UserLanguages[0];
                }
            }

            //// nao encontrou por COOKIE ou URL ou BROWSER
            if (langHeader.Length == 0)
            {
                langHeader = culturaPadrao;
            }

            //// verifica se esta na lista de cultura suportadas
            if (!listaSuportada.Contains(langHeader))
            {
                langHeader = culturaPadrao;            
            }

            //// seta a cultura selecionada
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(langHeader);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(langHeader);

            if (porCookieHabilitado)
            {
                //// save the location into cookie
                HttpCookie cookie = new HttpCookie("LinxB2C.CurrentUICulture", Thread.CurrentThread.CurrentUICulture.Name);
                cookie.Expires = DateTime.Now.AddYears(1);
                HttpContext.Response.SetCookie(cookie);
            }

            base.ExecuteCore();
        }
    }
}
