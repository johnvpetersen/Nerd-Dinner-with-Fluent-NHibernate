using System.Web.Mvc;
using NerdDinner.Controllers;
using NUnit.Framework;

namespace NerdDinner.Tests.Controllers {
    [TestFixture]
    public class HomeControllerTest {
        [Test]
        public void Index() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
        }

        [Test]
        public void About() {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
