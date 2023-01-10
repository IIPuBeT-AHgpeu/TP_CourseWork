using Microsoft.AspNetCore.Mvc;
using TP_CourseWork.Models;
using TP_CourseWork.Services;
using Image = TP_CourseWork.Models.Image;

namespace TP_CourseWork.Controllers
{
    public class HomeController : Controller
    {
        private IStrategyByPicture strategyByPicture;
        private readonly ILogger<HomeController> _Logger;
        private IPostgreSQLRepository _db;

        public HomeController(ILogger<HomeController> logger, IStrategyByPicture strategyByPicture, IPostgreSQLRepository db)
        {
            this._Logger = logger;
            this.strategyByPicture = strategyByPicture;
            _db = db;
        }

        #region Methods

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult History()
        {
            List<Recognize> recs = _db.GetAll().ToList();

            return View(recs);
        }

        [HttpPost]
        public ActionResult<int> Index(IFormFile image)
        {
            try
            {
                int count = 0;
                Recognize rec = new Recognize();

                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);

                    byte[] bytes = ms.ToArray();

                    rec.Image = bytes;

                    strategyByPicture = new FacesByPicture();
                    count = strategyByPicture.GetObjectsCount(new Image() { Data = bytes });
                    rec.Result = count;
                }

                _db.Create(rec);
                _db.Save();

                ViewBag.Num = count;
                return View();
            }
            catch (Exception e)
            {
                this._Logger.LogError($"[{nameof(this.Index)}] {e.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("Home/GetImage/{id}")]
        public IActionResult GetImage([FromRoute] int id)
        {
            Recognize rec = _db.GetOne(id);

            return File(rec.Image, "image/png");
        }

        #endregion     
    }
}