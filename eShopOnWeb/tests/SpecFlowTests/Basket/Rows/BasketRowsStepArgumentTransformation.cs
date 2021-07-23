using System.Collections.Generic;
using SpecFlowTests.Utils;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Basket.Rows
{
    [Binding]
    public class BasketRowsStepArgumentTransformation
    {
        //TODO: brauchen wir das?
        [StepArgumentTransformation]
        public AddItemToBasketRow ToAddItemToBasketRow(Table table) => table.CreateInstanceValidated<AddItemToBasketRow>();
        //TODO: ist validation noch gebraucht?

        [StepArgumentTransformation]
        public IEnumerable<AddItemToBasketRow> ToAddItemToBasketRows(Table table) => table.CreateSetValidated<AddItemToBasketRow>();

        [StepArgumentTransformation]
        public IEnumerable<AssertItemInBasketRow> ToAssertItemInBasketRows(Table table) => table.CreateSetValidated<AssertItemInBasketRow>();
    }
}