using System.Collections.Generic;
using System.Threading.Tasks;
using SpecFlowTests.Basket.Rows;
using SpecFlowTests.CurrentUser;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Basket
{
    [Binding]
    public class BasketSteps
    {
        private readonly BasketDriver _driver;
        private readonly CurrentUserContext _currentUserContext;

        public BasketSteps(BasketDriver driver, CurrentUserContext currentUserContext)
        {
            _driver = driver;
            _currentUserContext = currentUserContext;
        }

#region simple ones

        [Given(@"my basket contains '(.*)'")]
        public async Task GivenMyBasketContains(string itemName)
        {
            await _driver.GivenBasketOfUserContains(_currentUserContext.CurrentUserName, itemName);
        }

        [When(@"I add a '(.*)' to my basket")]
        public async Task WhenIAddAToMyBasket(string itemName)
        {
            await _driver.WhenIAddToBasketOfUser(_currentUserContext.CurrentUserName, itemName);
        }

        [Then(@"my basket contains '(.*)'")]
        public async Task ThenMyBasketContains(string[] itemNames)
        {
            await _driver.ThenBasketOfUserContains(_currentUserContext.CurrentUserName, itemNames);
        }

        [Then(@"the basket of '(.*)' is empty")]
        public async Task ThenTheBasketOfIsEmpty(string username)
        {
            await _driver.ThenTheBasketOfUserIsEmpty(username);
        }

        [Given(@"the basket of '(.*)' contains '(.*)'")]
        public async Task GivenTheBasketOfContains(string username, string itemName)
        {
            await _driver.GivenBasketOfUserContains(username, itemName);
        }

        [When(@"'(.*)' adds a '(.*)' to his basket")]
        public async Task WhenAddsAToHisBasket(string username, string itemName)
        {
            await _driver.WhenIAddToBasketOfUser(username, itemName);
        }

        [Then(@"the basket of '(.*)' contains '(.*)'")]
        public async Task ThenTheBasketOfContains(string username, string[] itemNames)
        {
            await _driver.ThenBasketOfUserContains(username, itemNames);
        }

        #endregion

#region complex ones

        [Given(@"my basket contains following products:")]
        public async Task GivenMyBasketContainsFollowingProducts(IEnumerable<AddItemToBasketRow> items)
        {
            await _driver.GivenBasketOfUserContainsFollowingProducts(_currentUserContext.CurrentUserName, items);
        }

        [When(@"I add the following product to my basket:")]
        public async Task WhenIAddTheFollowingProductToMyBasket(AddItemToBasketRow item)
        {
            await _driver.WhenIAddTheFollowingProductToBasketOfUser(_currentUserContext.CurrentUserName, item);
        }

        [Then(@"my basket contains following products:")]
        public async Task ThenMyBasketContainsFollowingProducts(IEnumerable<AssertItemInBasketRow> expected)
        {
            await _driver.ThenBasketOfUserContainsFollowingProducts(_currentUserContext.CurrentUserName, expected);
        }

#endregion
    }
}