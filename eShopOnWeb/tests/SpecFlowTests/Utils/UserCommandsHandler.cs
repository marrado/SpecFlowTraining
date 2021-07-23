using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.Pages.Basket;
using Microsoft.eShopWeb.Web.Services;
using Microsoft.eShopWeb.Web.ViewModels;

namespace SpecFlowTests.Utils
{
    public class UserCommandsHandler
    {
        private readonly WebTestFixture _testFixture;

        public UserCommandsHandler(WebTestFixture testFixture)
        {
            _testFixture = testFixture;
        }

        public async Task<List<CatalogItemViewModel>> GetAllCatalogItems()
        {
            var catalogItemService = _testFixture.ServiceProvider.GetService(typeof(ICatalogViewModelService)) as ICatalogViewModelService;
            return (await catalogItemService.GetCatalogItems(0, int.MaxValue, null, null)).CatalogItems;
        }

        public async Task<CatalogItemViewModel> GetItemByName(string name)
        {
            var catalogReadService = _testFixture.ServiceProvider.GetService(typeof(ICatalogViewModelService)) as ICatalogViewModelService;
            var allItems = await catalogReadService.GetCatalogItems(0, int.MaxValue, null, null);
            return allItems.CatalogItems.SingleOrDefault(item => item.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task AddToBasket(string username, int itemId, decimal itemPrice, int quantity)
        {
            var basketReadService = _testFixture.ServiceProvider.GetService(typeof(IBasketViewModelService)) as IBasketViewModelService;
            var basket = await basketReadService.GetOrCreateBasketForUser(username);
            var basketService = _testFixture.ServiceProvider.GetService(typeof(IBasketService)) as IBasketService;
            await basketService.AddItemToBasket(basket.Id, itemId, itemPrice, quantity);
        }

        public async Task<List<BasketItemViewModel>> GetItemsOfUser(string username)
        {
            var basketService = _testFixture.ServiceProvider.GetService(typeof(IBasketViewModelService)) as IBasketViewModelService;
            var basket = await basketService.GetOrCreateBasketForUser(username);
            return basket.Items;
        }

        
    }
}