using System.IO;
using System.Reflection;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace BaseProject.End2EndTests
{
    public class TestChrome
    {
        [Fact]
        public void PageLoads()
        {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:5000");
                var link = driver.FindElementByXPath("//body/nav/div/div/a");
                Assert.Contains("E2E", link.Text);
            }
        }
    }
}
