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
using TP_CourseWork.Services;
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
        private IStrategyByPicture strategyByPicture;
        private readonly ILogger<HomeController> _Logger;

        public HomeController(ILogger<HomeController> logger, IStrategyByPicture strategyByPicture)
        {
            this._Logger = logger;
            this.strategyByPicture = strategyByPicture;
        }

        #region Methods

        [Route("~/api/[controller]/Locations")] //
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<int> CountFacesByPicture(IFormFile image)
        {           
            try
            {
                int count = 0;
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);

                    strategyByPicture = new FacesByPicture();
                    count = strategyByPicture.GetObjectsCount(new Image() { Data = ms.ToArray() });
                }

                return Ok(count);
            }
            catch (Exception e)
            {
                this._Logger.LogError($"[{nameof(this.CountFacesByPicture)}] {e.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion     
    }
}