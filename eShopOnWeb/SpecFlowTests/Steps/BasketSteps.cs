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

        [When(@"ich einen Produkt in Einkaufswagen hinzufüge")]
        public void WennIchEinenProduktInEinkaufswagenHinzufuge()
        {
            _einkaufswagenDriver.AddDummyItemToBasket();
        }

        [Then(@"sollte mein Einkaufswagen (.*) Produkt beinhalten")]
        public void DannSollteMeinEinkaufswagenProduktBeinhalten(int itemCount)
        {
            _einkaufswagenDriver.AssertBasketContains(itemCount);
        }
    }
}