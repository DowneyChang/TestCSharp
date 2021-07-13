using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace WebApplication1.Controllers.Tests
{
    //1.每個「測試類別」都要有 TestClass 屬性(Attribute)
    [TestClass()]
    public class HomeControllerTests
    {
        //2.每個「測試方法」都要有TestMethod 屬性(Attribute)
        [TestMethod]
        public void Index()
        {
            // 3.每個「測試方法」的標準程式結構之 1：排列(Arrange) 
            //要先起始要進行測試的Function，或準備要執行該測試所需的變數
            HomeController controller = new HomeController();

            // 4.每個「測試方法」的標準程式結構之 2：作用(Act) 
            //執行要被測試的方法，並取得執行的結果
            ViewResult result = controller.Index() as ViewResult;

            //5.每個「測試方法」的標準程式結構之 3：判斷提示(Assert)
            //這部分負責用來判斷程式執行的結果是否符合預期！
            //這邊要判斷是否回傳值是Null
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.About() as ViewResult;

            // Assert
            //要判斷回傳字串與 Your application description page 是否相等
            Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }
    }
}