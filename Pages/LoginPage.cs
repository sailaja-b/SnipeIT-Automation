
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace SnipeITProject.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;

        // Constructor - receives the Playwright page instance
        public LoginPage(IPage page)
        {
            _page = page;
        }

        // Locators
        private ILocator EmailInput => _page.Locator("#username");
        private ILocator PasswordInput => _page.Locator("#password");
        private ILocator LoginButton => _page.Locator("#submit");

        // Login method
        public async Task LoginAsync(string email, string password)
        {
            await _page.GotoAsync("https://demo.snipeitapp.com/login");
            await EmailInput.FillAsync(email);
            await PasswordInput.FillAsync(password);
            await LoginButton.ClickAsync();

            // Optionally wait for navigation or some element that confirms login success
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }
    }
}