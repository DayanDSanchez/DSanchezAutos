﻿using System;
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
            ML.Auto auto = new ML.Auto();

            ML.Result result = BL.Auto.GetAllSP();
            if (result.Correct)
            {
                auto.Autos = result.Objects;
            }
            else
            {
                auto.Autos = new List<object>();
            }
            return View(auto);
        }

        [HttpGet]
        public ActionResult Form()
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
        public ActionResult Form(ML.Auto auto, HttpPostedFileBase ImagenAuto, string accion)
        {
            if (accion == "insert")
            {
                ML.Result result = BL.Auto.Add(auto);

                if (result.Correct)
                {
                    int idAuto = (int)result.Object;
                    if (Session["Imagenes"] != null)
                    {
                        List<object> imagenes = (List<object>)Session["Imagenes"];

                        foreach (var item in imagenes)
                        {
                            byte[] imagen = item as byte[];
                            BL.Auto.AddImagen(idAuto, imagen);
                        }

                        Session["Imagenes"] = null;
                    }
                }
                return RedirectToAction("GetAll");
            }
            if (accion == "addimagen")
            {
                var imagen = ConvertToArrayByte(ImagenAuto);

                if (Session["Imagenes"] == null)
                {
                    List<object> imagenes = new List<object>();
                    imagenes.Add(imagen);
                    Session["Imagenes"] = imagenes;
                }
                else
                {
                    List<object> imagenes = (List<object>)Session["Imagenes"];
                    imagenes.Add(imagen);
                    Session["Imagenes"] = imagenes;
                }

                //return RedirectToAction("Autos");
            }


            ML.Result resultMarca = BL.Marca.GetAllSP();
            if (resultMarca.Correct)
            {
                auto.Version.Modelo.Marca.Marcas = resultMarca.Objects;
            }
            else
            {
                auto.Version.Modelo.Marca.Marcas = new List<object>();
            }

            return View(auto);
        }


        [HttpPost]
        public ActionResult AddAuto(ML.Auto auto)
        {
            //BL
            ML.Result result = BL.Auto.Add(auto);

            if (result.Correct)
            {
                int idAuto = (int)result.Object;
                if (Session["imagenes"] != null)
                {
                    List<object> imagenes = (List<object>)Session["imagenes"];

                    foreach (var item in imagenes)
                    {
                        byte[] imagen = item as byte[];

                        BL.Auto.AddImagen(idAuto, imagen);
                    }
                }
            }
            return RedirectToAction("GetAll");
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
            }
            else
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