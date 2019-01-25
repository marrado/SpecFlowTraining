using System.Collections.Generic;
using FluentAssertions;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using SpecFlowTests.Infrastructure;

namespace SpecFlowTests.Drivers
{
    public class BasketDriver
    {
        private readonly DatabaseContext _dbContext;
        private readonly WebServiceContext _webContext;

        public BasketDriver(WebServiceContext webContext, DatabaseContext dbContext)
        {
            _webContext = webContext;
            _dbContext = dbContext;
        }

        public void AddDummyItemToBasket()
        {
            _dbContext.EnsureCatalogItemsExist(new List<CatalogItem> { TestDataProvider.GetDummyCatalogItem() });
            var catalogItemViewModel = TestDataProvider.GetDummyCatalogItemViewModel();

            _webContext.AddToBasket(catalogItemViewModel);
        }

        public void EnsureBasketEmpty()
        {
            _dbContext.EnsureBasketExists(TestConstants.TestUserId);
            var basketId = _dbContext.GetBasketId(TestConstants.TestUserId);
            _dbContext.EnsureBasketEmpty(basketId);
        }

        public void AssertBasketContains(int itemCount)
        {
            var basket = _dbContext.GetBasketForUser(TestConstants.TestUserId);
            basket.Items.Should().HaveCount(itemCount);
        }
    }
}