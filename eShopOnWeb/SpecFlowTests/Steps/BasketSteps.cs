using SpecFlowTests.Drivers;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Steps
{
    [Binding]
    public class BasketSteps
    {
        private readonly BasketDriver _EinkaufswagenDriver;

        public BasketSteps(BasketDriver EinkaufswagenDriver)
        {
            _EinkaufswagenDriver = EinkaufswagenDriver;
        }

        [Given(@"mein Einkaufswagen ist leer")]
        public void AngenommenMeinEinkaufswagenIstLeer()
        {
            _EinkaufswagenDriver.EnsureBasketEmpty();
        }

        [When(@"ich einen Produkt in Einkaufswagen hinzufüge")]
        public void WennIchEinenProduktInEinkaufswagenHinzufuge()
        {
            _EinkaufswagenDriver.AddDummyItemToBasket();
        }

        [Then(@"sollte mein Einkaufswagen (.*) Produkt beinhalten")]
        public void DannSollteMeinEinkaufswagenProduktBeinhalten(int itemCount)
        {
            _EinkaufswagenDriver.AssertBasketContains(itemCount);
        }
    }
}