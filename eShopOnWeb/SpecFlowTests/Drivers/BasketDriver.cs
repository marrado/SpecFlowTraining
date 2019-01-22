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

        public void AddDummyItemsToBasket(int itemCount)
        {
            _dbContext.EnsureCatalogItemsExist(TestDataProvider.GetDummyCatalogItems(itemCount));

            TestDataProvider.GetDummyCatalogItemViewModels(itemCount).ForEach(item => _webContext.AddToBasket(item));
        }

        public void EnsureBasketEmpty()
        {
            var basket = _webContext.GetBasket();
            _dbContext.EnsureBasketEmpty(basket.Id);
        }

        public void AssertBasketContains(int itemCount)
        {
            var basket = _webContext.GetBasket();
            basket.ItemsCount.Should().Be(itemCount);
        }

        public void EnsureBasketContainsItems(int itemCount)
        {
            var basket = _webContext.GetBasket();
            _dbContext.EnsureBasketContainsOnlyItems(basket.Id, TestDataProvider.GetDummyCatalogItems(itemCount));
        }
    }
}