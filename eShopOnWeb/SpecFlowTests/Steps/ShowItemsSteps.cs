using SpecFlowTests.Drivers;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Steps
{
    /// <summary>
    /// Binding class for the steps connected to Shwoing available items on the main page.
    /// It connects the test definition (feature file) with implementation (driver file)
    /// and defines what is done (not how) to fulfill the command from feature file.
    /// </summary>
    [Binding]
    public class ShowItemsSteps
    {
        private readonly ShowItemsDriver _warenAnzeigenDriver;

        public ShowItemsSteps(ShowItemsDriver warenAnzeigenDriver)
        {
            _warenAnzeigenDriver = warenAnzeigenDriver;
        }

        [Given(@"eine leere in-Memory Datenbank")]
        public void AngenommenEineLeereIn_MemoryDatenbank()
        {
            _warenAnzeigenDriver.EnsureItemsCatalogEmpty();
        }

        [Given(@"es (.*) Waren in dem Katalog gibt")]
        public void AngenommenEsWarenInDemKatalogGibt(int itemsCount)
        {
            _warenAnzeigenDriver.EnsureCatalogContainsDummyItems(itemsCount);
        }


        [When(@"ich die erste Seite von Waren anfordere")]
        public void WennIchDieErsteWarenAnfordere()
        {
            _warenAnzeigenDriver.ShowFirstPage();
        }

        [Then(@"sollten alle diese Waren angezeigt werden")]
        public void DannSolltenDieseWarenAngezeigt()
        {
            _warenAnzeigenDriver.AssertExpectedItemsAreListed();
        }
    }
}