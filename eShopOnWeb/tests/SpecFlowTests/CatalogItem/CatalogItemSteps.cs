using System.Collections.Generic;
using SpecFlowTests.CatalogItem.Rows;
using TechTalk.SpecFlow;

namespace SpecFlowTests.CatalogItem
{
    [Binding]
    public class CatalogItemSteps
    {
        private readonly CatalogItemDriver _driver;

        public CatalogItemSteps(CatalogItemDriver driver)
        {
            _driver = driver;
        }

        [When(@"I add the following item to the catalog:")]
        public void WhenIAddTheFollowingItemToTheCatalog(AddItemToCatalogRow item)
        {
            _driver.WhenIAddTheFollowingItemToTheCatalog(item);
        }

        [Then(@"the catalog contains the following items:")]
        public void ThenTheCatalogContainsTheFollowingItems(IEnumerable<AssertItemInCatalogRow> expected)
        {
            _driver.ThenTheCatalogContainsTheFollowingItems(expected);
        }

        [Then(@"the catalog does not contain the following items:")]
        public void ThenTheCatalogDoesNotContainTheFollowingItems(IEnumerable<AssertItemInCatalogRow> notExpected)
        {
            _driver.ThenTheCatalogDoesNotContainTheFollowingItems(notExpected);
        }
    }
}