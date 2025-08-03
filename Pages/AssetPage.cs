
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SnipeITProject.Pages
{
    public class AssetPage
    {
        private readonly IPage Page;

        public AssetPage(IPage page)
        {
            Page = page;
        }

        // Locators
        private ILocator CreateNewBtn => Page.Locator("button[name='btnAdd'][title='Create New']");
        private ILocator CreateAssetLink => Page.Locator("a:has-text('Asset')");
        private ILocator ModelDropdown => Page.Locator("#model_id + span"); // Select2 dropdown trigger
        private ILocator ModelSearchInput => Page.Locator(".select2-search__field"); // input for searching in dropdown
        private ILocator StatusDropdown => Page.Locator("#status_id");
        private ILocator CheckoutToDropdown => Page.Locator("#assigned_to");
        private ILocator SaveBtn => Page.Locator("button:has-text('Save')");
        private ILocator SuccessMsg => Page.Locator(".alert-success");
        private ILocator AssetsTopLink => Page.Locator("a[data-title='Assets']");
        private ILocator SearchInput => Page.Locator("input[type='search']");

        // 1. Open create asset form
        public async Task OpenCreateAssetFormAsync()
        {
            await CreateNewBtn.ClickAsync();
            await CreateAssetLink.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        // 2. Fill asset form
        public async Task FillCreateAssetFormAsync(string modelName, string status, string checkoutUser)
        {
            // Select Model from Select2 dropdown
            await ModelDropdown.ClickAsync();
            await ModelSearchInput.FillAsync(modelName);
            await Page.Locator($".select2-results__option:has-text('{modelName}')").ClickAsync();

            // Select Status from native select dropdown
            await StatusDropdown.SelectOptionAsync(new SelectOptionValue { Label = status });

            // Select Checkout To from native select dropdown
            await CheckoutToDropdown.SelectOptionAsync(new SelectOptionValue { Label = checkoutUser });
        }

        // 3. Save asset
        public async Task SaveAssetAsync()
        {
            await SaveBtn.ClickAsync();
        }

        // 4. Verify success message contains "Success"
        public async Task<bool> IsSuccessMessageDisplayedAsync()
        {
            await SuccessMsg.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible, Timeout = 10000 });
            var text = await SuccessMsg.InnerTextAsync();
            return text.Contains("Success");
        }

        // 5. Go to asset list page
        public async Task GoToAssetsListAsync()
        {
            await AssetsTopLink.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        // 6. Search asset by model name (or any unique part)
        public async Task<bool> IsAssetInListAsync(string modelName)
        {
            await SearchInput.FillAsync(modelName);
            await Page.WaitForTimeoutAsync(2000); // wait for search results to appear
            return await Page.Locator($"text={modelName}").IsVisibleAsync();
        }

        // 7. Open asset details by clicking asset name link
        public async Task OpenAssetDetailsAsync(string modelName)
        {
            await Page.Locator($"a:has-text('{modelName}')").ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        }

        // 8. Validate asset details on the detail page (basic example)
        public async Task<bool> ValidateAssetDetailsAsync(string expectedModel)
        {
            var modelText = await Page.Locator("dd#model").InnerTextAsync();
            return modelText.Contains(expectedModel);
        }

        // 9. Validate "History" tab content
        public async Task<bool> ValidateHistoryTabAsync(string expectedEntry)
        {
            await Page.Locator("a:has-text('History')").ClickAsync();
            await Page.WaitForTimeoutAsync(1000);
            return await Page.Locator($"text={expectedEntry}").IsVisibleAsync();
        }
    }
}