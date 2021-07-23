using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using SpecFlowTests.Basket.Rows;
using SpecFlowTests.Setup;
using SpecFlowTests.Utils;

namespace SpecFlowTests.Basket
{
    public class BasketDriver
    {
        private readonly UserCommandsHandler _executor;

        public BasketDriver(UserCommandsHandler executor)
        {
            _executor = executor;
        }

        public async Task GivenBasketOfUserContains(string username, string itemName)
        {
            await WhenIAddToBasketOfUser(username, itemName);
            await ThenBasketOfUserContains(username, itemName);
        }

        public async Task WhenIAddToBasketOfUser(string username, string itemName)
        {
            var itemModel = await _executor.GetItemByName(itemName);
            await _executor.AddToBasket(username, itemModel.Id, itemModel.Price, 1);
        }

        public async Task ThenBasketOfUserContains(string username, params string[] itemNames)
        {
            var itemsInBasket = await _executor.GetItemsOfUser(username);
            itemsInBasket.Select(i => i.ProductName).Should().BeEquivalentTo(itemNames);
        }

        public async Task ThenTheBasketOfUserIsEmpty(string username)
        {
            var itemsInBasket = await _executor.GetItemsOfUser(username);
            itemsInBasket.Should().BeEmpty();
        }

        //------------------------------------------------------------------------------------------------------------------------

        public async Task GivenBasketOfUserContainsFollowingProducts(string username, IEnumerable<AddItemToBasketRow> items)
        {
            foreach (var item in items)
            {
                await WhenIAddTheFollowingProductToBasketOfUser(username, item);
            }

            await ThenBasketOfUserContainsFollowingProducts(username, items.Select(i => new AssertItemInBasketRow
            {
                Name = i.Name,
                Amount = i.Amount
            }));
        }

        public async Task WhenIAddTheFollowingProductToBasketOfUser(string username, AddItemToBasketRow item)
        {
            var itemModel = await _executor.GetItemByName(item.Name);
            await _executor.AddToBasket(username, itemModel.Id, itemModel.Price, item.Amount);
        }

        public async Task ThenBasketOfUserContainsFollowingProducts(string username, IEnumerable<AssertItemInBasketRow> expected)
        {
            var itemsOfUser = await _executor.GetItemsOfUser(username);
            itemsOfUser.Select(item => new AssertItemInBasketRow
            {
                Name = item.ProductName,
                Amount = item.Quantity
            }).Should().BeEquivalentTo(expected);
        }
    }
}