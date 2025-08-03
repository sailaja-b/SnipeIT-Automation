

using NUnit.Framework;
using System;
using System.Threading.Tasks;
using SnipeITProject.Pages;

namespace SnipeITProject.Test
{
    [TestFixture]
    public class AssetTests : BaseTest
    {
        [Test]
        public async Task CreateAndValidateAssetTest()
        {
            var assetPage = new AssetPage(Page);

            // Open create new asset form
            await assetPage.OpenCreateAssetFormAsync();

            // Fill form
            string modelName = "Macbook Pro 13";
            string status = "Ready to Deploy";

            // Use a valid username from dropdown (hardcoded or dynamic list)
            string checkoutUser = "admin"; // adjust to any user in the list visible in dropdown

            await assetPage.FillCreateAssetFormAsync(modelName, status, checkoutUser);

            // Save asset
            await assetPage.SaveAssetAsync();

            // Validate success message
            Assert.IsTrue(await assetPage.IsSuccessMessageDisplayedAsync(), "Success message not shown.");

            // Go to asset list page
            await assetPage.GoToAssetsListAsync();

            // Validate the created asset in the list
            Assert.IsTrue(await assetPage.IsAssetInListAsync(modelName), $"Asset '{modelName}' not found in list.");

            // Open asset details page
            await assetPage.OpenAssetDetailsAsync(modelName);

            // Validate asset details
            Assert.IsTrue(await assetPage.ValidateAssetDetailsAsync(modelName), "Asset details validation failed.");

            // Validate history tab has 'created' entry
            Assert.IsTrue(await assetPage.ValidateHistoryTabAsync("created"), "History tab validation failed.");
        }
    }
}