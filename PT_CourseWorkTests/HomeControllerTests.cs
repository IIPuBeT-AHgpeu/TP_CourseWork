using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TP_CourseWork.Controllers;
using TP_CourseWork.Models;
using TP_CourseWork.Services;

namespace PT_CourseWorkTests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void IndexControllerTest()
        {
            var logger = new Mock<ILogger<HomeController>>();
            var strategy = new Mock<IStrategyByPicture>();
            var repository = new Mock<IPostgreSQLRepository>();
            var controller = new HomeController(logger.Object, strategy.Object, repository.Object);
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HistoryControllerTest()
        {
            var logger = new Mock<ILogger<HomeController>>();
            var strategy = new Mock<IStrategyByPicture>();
            var repository = new Mock<IPostgreSQLRepository>();
            var controller = new HomeController(logger.Object, strategy.Object, repository.Object);
            ViewResult result = controller.History() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void IndexPostControllerTest()
        {
            var logger = new Mock<ILogger<HomeController>>();
            var strategy = new Mock<IStrategyByPicture>();
            var repository = new Mock<IPostgreSQLRepository>();
            var controller = new HomeController(logger.Object, strategy.Object, repository.Object);

            var img = new Mock<IFormFile>();

            ActionResult<int> result = controller.Index(img.Object);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetImageControllerTest()
        {
            var logger = new Mock<ILogger<HomeController>>();
            var strategy = new Mock<IStrategyByPicture>();
            var repository = new Mock<IPostgreSQLRepository>();
            var controller = new HomeController(logger.Object, strategy.Object, repository.Object);

            repository.Setup(r => r.GetOne(1)).Returns(new Recognize() { Image = new byte[1024] });

            var img = new Mock<IFormFile>();

            var result = controller.GetImage(1);
            Assert.IsNotNull(result);
        }
    }
}