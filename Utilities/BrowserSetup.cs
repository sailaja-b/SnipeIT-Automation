
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SnipeITProject.Utilities;
    public static class PlaywrightDriver
    {
       public static async Task<IBrowser> LaunchBrowserAsync(string browserType = "chromium", bool headless = true)
{
    var playwright = await Playwright.CreateAsync();

    if (string.Equals(browserType, "firefox", StringComparison.OrdinalIgnoreCase))
    {
        return await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless });
    }
    else if (string.Equals(browserType, "webkit", StringComparison.OrdinalIgnoreCase))
    {
        return await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless });
    }
    else // default to Chromium
    {
        return await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = headless });
    }
}
    }
