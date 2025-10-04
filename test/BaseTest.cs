using Microsoft.Playwright;
using NUnit.Framework;
using SnipeITProject.Utilities; // for PlaywrightDriver

namespace SnipeITProject.Test
{
    public class BaseTest
    {
        protected IBrowser Browser;
        protected IPage Page;
        protected IBrowserContext Context;

        [SetUp]
        public async Task Setup()
        {
            Browser = await PlaywrightDriver.LaunchBrowserAsync("chromium", false); // set true if headless
            Context = await Browser.NewContextAsync();
            Page = await Context.NewPageAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            await Page.CloseAsync();
            await Context.CloseAsync();
            await Browser.CloseAsync();
        }
    }
}