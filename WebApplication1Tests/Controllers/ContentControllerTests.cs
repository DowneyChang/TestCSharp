using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace WebApplication1.Controllers.Tests
{
    [TestClass()]
    public class ContentControllerTests
    {
        [TestMethod()]
        public void DatepickerTest()
        {
            ContentController controller = new ContentController();
            ViewResult result = controller.Datepicker() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}