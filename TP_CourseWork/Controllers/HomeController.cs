using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DlibDotNet;
//using FaceDetection.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Image = TP_CourseWork.Models.Image;

namespace TP_CourseWork.Controllers
{
    /// <summary>
    /// Get rectangles of face area from specified image
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {

        #region Fields

        private readonly ILogger<HomeController> _Logger;

        #endregion

        #region Constructors

        public HomeController(ILogger<HomeController> logger)
        {
            this._Logger = logger;
        }

        #endregion

        #region Properties
        #endregion

        #region Methods

        /// <summary>
        /// Returns an enumerable collection of face location correspond to all faces in specified image.
        /// </summary>
        [Route("~/api/[controller]/Locations")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<int> Locations(IFormFile image)
        {
            int count = 0;
            Console.WriteLine($"Tut: {image}");
            try
            {

                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);

                    using (var bitmap = (Bitmap)System.Drawing.Image.FromStream(ms))
                    using (var png = new MemoryStream())
                    {
                        bitmap.Save(png, ImageFormat.Png);
                        using (var matrix = Dlib.LoadPng<BgrPixel>(png.ToArray()))
                        using (var faceDetector = Dlib.GetFrontalFaceDetector())
                        {
                            var dets = faceDetector.Operator(matrix);
                            count = dets.Count();
                        }
                    }
                }

                return Ok(count);
            }
            catch (Exception e)
            {
                this._Logger.LogError($"[{nameof(this.Locations)}] {e.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion     
    }
}