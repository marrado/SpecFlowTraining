using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.Web;
using Microsoft.eShopWeb.Web.ViewModels;
using SpecFlowTests.Infrastructure;

namespace SpecFlowTests.Drivers
{
    /// <summary>
    /// Driver class implementing the steps for showing items on the main page.
    /// </summary>
    public class ShowItemsDriver
    {
        /// <summary>
        /// Contexts, in which the test works. They are initialized using SpecFlow Dependecy Injection.
        /// All the drivers share the contexts within one test case. That assures consistency between drivers.
        /// </summary>
        private readonly DatabaseContext _dbContext;
        private readonly WebServiceContext _serviceContext;

        private List<CatalogItem> _expectedItems;
        private IEnumerable<CatalogItemViewModel> _shownItems;
        private int _pageSize;

        public ShowItemsDriver(DatabaseContext dbContext, WebServiceContext serviceContext)
        {
            _dbContext = dbContext;
            _serviceContext = serviceContext;
            _pageSize = Constants.DefaultCatalogPageSize;
        }

        public void EnsureItemsCatalogEmpty()
        {
            _dbContext.EnsureDatabaseIsEmpty();
            _expectedItems = new List<CatalogItem>();
        }

        public void EnsureCatalogContainsDummyItems(int itemsCount)
        {
            if (itemsCount == 0)
            {
                EnsureItemsCatalogEmpty();
            }
            else
            {
                var catalogItems = TestDataProvider.GetDummyCatalogItems(itemsCount);

                _expectedItems.AddRange(catalogItems);
                _dbContext.EnsureCatalogItemsExist(catalogItems);
            }
        }

        public void AssertExpectedItemsAreListed()
        {
            _shownItems.Select(item => item.Id).Should().BeEquivalentTo(_expectedItems.Select(item => item.Id));
        }

        public void AssertExpectedItemCountIsListed(int itemsCount)
        {
            _shownItems.Count().Should().Be(itemsCount);
        }

        public void ShowFirstPage()
        {
            var model = _serviceContext.GetFirstCatalogPage(_pageSize);
            _shownItems = model.CatalogItems;
        }

        public void ShowPage(int pageNumber)
        {
            var model = _serviceContext.GetCatalogPage(pageNumber - 1, _pageSize);
            _shownItems = model.CatalogItems;
        }

        public void SetPageSize(int pageSize)
        {
            _pageSize = pageSize;
        }
    }
}