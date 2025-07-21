using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class AutoController : Controller
    {
        // GET: Auto
        public ActionResult GetAll()
        {
            return View();
        }

        public ActionResult Autos()
        {
            ML.Auto auto = new ML.Auto();
            auto.Version = new ML.Version();
            auto.Version.Modelo = new ML.Modelo();
            auto.Version.Modelo.Marca = new ML.Marca();

            //ML.Marca marca = new ML.Marca();

            ML.Result result = BL.Marca.GetAllSP();
            if (result.Correct)
            {
                auto.Version.Modelo.Marca.Marcas = result.Objects;
            }
            else
            {
                auto.Version.Modelo.Marca.Marcas = new List<object>();
            }

            return View(auto);
        }

        [HttpPost]
        public ActionResult CargarImagen(HttpPostedFileBase ImagenAuto)
        {
            var imagen = ConvertToArrayByte(ImagenAuto);

            if (Session["Imagenes"] == null)
            {
                List<object> imagenes = new List<object>();
                imagenes.Add(imagen);

                Session["Imagenes"] = imagenes;
            } else
            {
                List<object> imagenes = (List<object>)Session["imagenes"];
                imagenes.Add(imagen);

                Session["Imagenes"] = imagenes;
            }
            return RedirectToAction("Autos");
        }

        public byte[] ConvertToArrayByte(HttpPostedFileBase imagen)
        {
            using (Stream inputStream = imagen.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                return memoryStream.ToArray();
            }
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