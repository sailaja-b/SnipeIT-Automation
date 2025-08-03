
using NUnit.Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;
using SnipeITProject.Pages;

namespace SnipeITProject.Tests
{
    public class LoginTests
    {
        private IBrowser _browser;
        private IPage _page;
        private IPlaywright _playwright;

        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var context = await _browser.NewContextAsync();
            _page = await context.NewPageAsync();
        }

        [Test]
        public async Task ValidLoginTest()
        {
            var loginPage = new LoginPage(_page);
            await loginPage.LoginAsync("admin", "password");

            // Add an assertion if needed, like checking for dashboard element
            var pageTitle = await _page.TitleAsync();
           // Assert.IsTrue(pageTitle.Contains("Dashboard")); // optional
        }

        [TearDown]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}