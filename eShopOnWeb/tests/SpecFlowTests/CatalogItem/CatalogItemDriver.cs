using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using SpecFlowTests.CatalogItem.Rows;
using SpecFlowTests.Utils;

namespace SpecFlowTests.CatalogItem
{
    public class CatalogItemDriver
    {
        private readonly AdminCommandsHandler _executor;

        public CatalogItemDriver(AdminCommandsHandler executor)
        {
            _executor = executor;
        }

        public async Task WhenIAddTheFollowingItemToTheCatalog(AddItemToCatalogRow item)
        {
            var brand = await _executor.GetCatalogBrandByNameAdmin(item.Brand);
            var type = await _executor.GetCatalogTypeByNameAdmin(item.Type);
            await _executor.AddCatalogItem(item.Name, item.Price, item.Description, brand?.Id ?? 0, type?.Id ?? 0);
        }

        public async Task ThenTheCatalogContainsTheFollowingItems(IEnumerable<AssertItemInCatalogRow> expected)
        {
            var allCatalogItems = await _executor.GetAllCatalogItemsAdmin();

            allCatalogItems.Select(i => new AssertItemInCatalogRow
            {
                Name = i.Name,
                Price = i.Price,
                Description = i.Description,
                Brand = i.CatalogBrand,
                Type = i.CatalogType
            }).Should().Contain(expected);
        }

        public async Task ThenTheCatalogDoesNotContainTheFollowingItems(IEnumerable<AssertItemInCatalogRow> notExpected)
        {
            var allCatalogItems = await _executor.GetAllCatalogItemsAdmin();

            allCatalogItems.Select(i => new AssertItemInCatalogRow
            {
                Name = i.Name,
                Price = i.Price,
                Description = i.Description,
                Brand = i.CatalogBrand,
                Type = i.CatalogType
            }).Should().NotContain(notExpected);
        }
    }
}