using System.IO;
using System.Linq;
using System.Reflection;
using BaseProject.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
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


        [Fact]
        public void CreatingNewIssueSavesCorrectly()
        {
            var data = new
            {
                Title = "Issue",
                Description = "Fix all the bugs",
                Status = IssueStatus.Done
            };
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)))
            {
                driver.Navigate().GoToUrl(@"https://localhost:5000");
                // Using this below to verify the record we created is new and not an old one
                var existing = driver.FindElementsByCssSelector("tbody td a[href^='/Issue/Edit/']");
                var existingIds = existing.Select(x => x.GetAttribute("href").Substring(x.GetAttribute("href").LastIndexOf("/") + 1)).Select(x => int.Parse(x)).ToList();

                var createNewLink = driver.FindElementByLinkText("Create New");
                Assert.Contains("Create New", createNewLink.Text);
                createNewLink.Click();
                /* PAGE CHANGE */
                Assert.Equal(@"https://localhost:5000/Issue/Create", driver.Url);
                var titleInput = driver.FindElementById("Title");
                titleInput.SendKeys(data.Title);
                var descriptionInput = driver.FindElementById("Description");
                descriptionInput.SendKeys(data.Description);
                var statusSelect = driver.FindElementById("Status");
                var selectElement = new SelectElement(statusSelect);
                selectElement.SelectByValue($"{(int)data.Status}");
                var createButton = driver.FindElementByCssSelector("input.btn.btn-default");
                Assert.Equal("Create", createButton.GetAttribute("value"));
                createButton.Click();
                /* PAGE CHANGE */
                Assert.Equal(@"https://localhost:5000/Issue", driver.Url);
                var table = driver.FindElementByTagName("tbody");
                var rows = table.FindElements(By.CssSelector("tr"));
                foreach (var row in rows)
                {
                    var columns = row.FindElements(By.TagName("td"));
                    var col1 = columns.First();
                    var col2 = columns.Skip(1).First();
                    var col3 = columns.Skip(2).First();

                    var edit = col3.FindElement(By.CssSelector("a[href^='/Issue/Edit/']"));
                    var editHref = edit.GetAttribute("href");
                    var editValue = edit.Text;
                    var editIdString = editHref.Substring(editHref.LastIndexOf("/") + 1);
                    var editId = int.Parse(editIdString);
                    if (existingIds.Contains(editId))
                        continue;

                    Assert.Equal(data.Title, col1.Text);
                    Assert.Equal(data.Description, col2.Text);
                    return;
                }
                throw new NotFoundException("Did not find new Issue in table");
            }
        }
    }
}
