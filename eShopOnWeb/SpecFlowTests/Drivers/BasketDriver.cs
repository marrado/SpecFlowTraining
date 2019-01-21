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

        public void AddDummyItemToBasket()
        {
            _dbContext.EnsureCatalogItemsExist(new[] { TestDataProvider.GetDummyCatalogItem() });
            var catalogItemViewModel = TestDataProvider.GetDummyCatalogItemViewModel();

            _webContext.AddToBasket(catalogItemViewModel);
        }

        public void EnsureBasketEmpty()
        {
            _dbContext.EnsureNoBasketsExist();
        }

        public void AssertBasketContains(int itemCount)
        {
            var basket = _webContext.GetBasket();
            basket.Items.Should().HaveCount(itemCount);
        }
    }
}