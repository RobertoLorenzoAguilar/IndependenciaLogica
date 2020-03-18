using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Datos;
using Newtonsoft.Json;

namespace IndependenciaLogicaBD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<string> lstUsuariosAtr = BaseDatos.Main();
            //ViewBag.JsonObject =
            //JsonConvert.SerializeObject(lstUsuariosAtr, Formatting.Indented,
            //      new JsonSerializerSettings()
            //      {
            //          ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            //      });      
            ViewBag.JsonObject = lstUsuariosAtr;
            return View();
        }
    }
}