using SpecFlowTests.Drivers;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Steps
{
    [Binding]
    public class BasketSteps
    {
        private readonly BasketDriver _einkaufswagenDriver;

        public BasketSteps(BasketDriver einkaufswagenDriver)
        {
            _einkaufswagenDriver = einkaufswagenDriver;
        }

        [Given(@"mein Einkaufswagen ist leer")]
        public void AngenommenMeinEinkaufswagenIstLeer()
        {
            _einkaufswagenDriver.EnsureBasketEmpty();
        }

        [Given(@"mein Einkaufswagen hat schon (.*) Produkte")]
        public void AngenommenMeinEinkaufswagenHatSchonProdukte(int itemCount)
        {
            _einkaufswagenDriver.EnsureBasketContainsItems(itemCount);
        }

        [When(@"ich (.*) Produkte in Einkaufswagen hinzufüge")]
        public void WennIchProdukteInEinkaufswagenHinzufuge(int itemCount)
        {
            _einkaufswagenDriver.AddDummyItemsToBasket(itemCount);
        }

        [Then(@"sollte mein Einkaufswagen (.*) Produkte beinhalten")]
        public void DannSollteMeinEinkaufswagenProduktBeinhalten(int itemCount)
        {
            _einkaufswagenDriver.AssertBasketContains(itemCount);
        }
    }
}