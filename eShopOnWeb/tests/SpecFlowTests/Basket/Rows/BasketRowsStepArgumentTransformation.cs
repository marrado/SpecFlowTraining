using System.Collections.Generic;
using SpecFlowTests.Utils;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Basket.Rows
{
    [Binding]
    public class BasketRowsStepArgumentTransformation
    {
        [StepArgumentTransformation]
        public AddItemToBasketRow ToAddItemToBasketRow(Table table) => table.CreateInstanceValidated<AddItemToBasketRow>();

        [StepArgumentTransformation]
        public IEnumerable<AddItemToBasketRow> ToAddItemToBasketRows(Table table) => table.CreateSetValidated<AddItemToBasketRow>();

        [StepArgumentTransformation]
        public IEnumerable<AssertItemInBasketRow> ToAssertItemInBasketRows(Table table) => table.CreateSetValidated<AssertItemInBasketRow>();
    }
}