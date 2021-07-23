using System.Collections.Generic;
using SpecFlowTests.CatalogItem.Rows;
using SpecFlowTests.Utils;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Basket.Rows
{
    [Binding]
    public class CatalogItemRowsStepArgumentTransformation
    {
        [StepArgumentTransformation]
        public AddItemToCatalogRow ToAddItemToCatalogRow(Table table) => table.CreateInstanceValidated<AddItemToCatalogRow>();

        [StepArgumentTransformation]
        public IEnumerable<AddItemToCatalogRow> ToAddItemToCatalogRows(Table table) => table.CreateSetValidated<AddItemToCatalogRow>();

        [StepArgumentTransformation]
        public IEnumerable<AssertItemInCatalogRow> ToAssertItemInCatalogRows(Table table) => table.CreateSetValidated<AssertItemInCatalogRow>();
    }
}