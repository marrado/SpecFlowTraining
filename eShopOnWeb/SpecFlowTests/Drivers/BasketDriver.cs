using System.Linq;
using FluentAssertions;
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

        public void EnsureBasketEmpty()
        {
            _dbContext.EnsureBasketExists(TestConstants.TestUserId);
            var basketId = _dbContext.GetBasketId(TestConstants.TestUserId);
            _dbContext.EnsureBasketEmpty(basketId);
        }

        public void EnsureBasketContainsItems(int itemCount)
        {
            _dbContext.EnsureBasketExists(TestConstants.TestUserId);
            var basketId = _dbContext.GetBasketId(TestConstants.TestUserId);
            _dbContext.EnsureBasketContainsOnlyItems(basketId, TestDataProvider.GetDummyCatalogItems(itemCount));
        }

        public void AddDummyItemsToBasket(int itemCount)
        {
            _dbContext.EnsureCatalogItemsExist(TestDataProvider.GetDummyCatalogItems(itemCount));
            TestDataProvider.GetDummyCatalogItemViewModels(itemCount).ForEach(item => _webContext.AddToBasket(item));
        }

        public void AssertBasketContains(int itemCount)
        {
            var basket = _dbContext.GetBasketForUser(TestConstants.TestUserId);
            basket.Items.Sum(i => i.Quantity).Should().Be(itemCount);
        }
    }
}