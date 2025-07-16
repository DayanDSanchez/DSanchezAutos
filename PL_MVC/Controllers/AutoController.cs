using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class AutoController : Controller
    {
        // GET: Auto
        public ActionResult Autos()
        {
            ML.Version version = new ML.Version();
            version.Modelo = new ML.Modelo();
            version.Modelo.Marca = new ML.Marca();
            //ML.Marca marca = new ML.Marca();

            ML.Result result = BL.Marca.GetAllSP();
            if (result.Correct)
            {
                version.Modelo.Marca.Marcas = result.Objects;
            }
            else
            {
                version.Modelo.Marca.Marcas = new List<object>();
            }

            return View(version);
        }

        public JsonResult ModelosGetByIdMarca(int IdMarca)
        {
            ML.Result result = BL.Modelo.ModeloGetByIdMarca(IdMarca);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VersionesGetByIdModelo(int IdModelo)
        {
            ML.Result result = BL.Version.VersionByIdModelo(IdModelo);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}