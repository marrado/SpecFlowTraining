using SpecFlowTests.Drivers;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Steps
{
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

        [Given(@"es bis (.*) Waren auf einer Seite gibt")]
        public void AngenommenEsBisWarenAufEinerSeiteGibt(int pageSize)
        {
            _warenAnzeigenDriver.SetPageSize(pageSize);
        }


        [When(@"ich die erste Seite von Waren anfordere")]
        public void WennIchDieErsteWarenAnfordere()
        {
            _warenAnzeigenDriver.ShowFirstPage();
        }

        [When(@"ich Seite (.*) von Waren anfordere")]
        public void WennIchSeiteVonWarenAnfordere(int pageNumber)
        {
            _warenAnzeigenDriver.ShowPage(pageNumber);
        }

        [Then(@"sollten alle diese Waren angezeigt werden")]
        public void DannSolltenDieseWarenAngezeigt()
        {
            _warenAnzeigenDriver.AssertExpectedItemsAreListed();
        }

        [Then(@"sollen (.*) Waren angezeigt werden")]
        public void DannSollenAufSeiteAngezeigtWerden(int itemsCount)
        {
            _warenAnzeigenDriver.AssertExpectedItemCountIsListed(itemsCount);
        }
    }
}