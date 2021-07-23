using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorShared.Interfaces;
using BlazorShared.Models;

namespace SpecFlowTests.Setup
{
    //TODO not properly mocked yet
    public class AdminCommandsHandler
    {
        private readonly WebTestFixture _testFixture;

        public AdminCommandsHandler(WebTestFixture testFixture)
        {
            _testFixture = testFixture;
        }

        public async Task AddCatalogItem(string name, decimal price, string description, int catalogBrandId,
            int catalogTypeId)
        {

            var catalogItemService = _testFixture.ServiceProvider.GetService(typeof(ICatalogItemService)) as ICatalogItemService;
            await catalogItemService.Create(new CreateCatalogItemRequest
            {
                Price = price,
                Name = name,
                Description = description,
                PictureBase64 = null,
                PictureName = null,
                PictureUri = null,
                CatalogBrandId = catalogBrandId,
                CatalogTypeId = catalogTypeId,
            });
        }

        public async Task<List<BlazorShared.Models.CatalogItem>> GetAllCatalogItems()
        {
            var catalogItemService = _testFixture.ServiceProvider.GetService(typeof(ICatalogItemService)) as ICatalogItemService;
            return await catalogItemService.List();
        }

        public async Task<CatalogBrand> GetCatalogBrandByName(string name)
        {
            var catalogItemService = _testFixture.ServiceProvider.GetService(typeof(ICatalogBrandService)) as ICatalogBrandService;
            return (await catalogItemService.List()).SingleOrDefault(b => b.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<CatalogType> GetCatalogTypeByName(string name)
        {
            var catalogItemService = _testFixture.ServiceProvider.GetService(typeof(ICatalogTypeService)) as ICatalogTypeService;
            return (await catalogItemService.List()).SingleOrDefault(t => t.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}