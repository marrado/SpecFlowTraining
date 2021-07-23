using TechTalk.SpecFlow;

namespace SpecFlowTests.Utils.Common.StepArgumentTransformations
{
    [Binding]
    public class StringToDecimalStepArgumentTransformation
    {
        [StepArgumentTransformation]
        public decimal? NullableDecimalTransformation(string value)
        {
            if (MapDriver.IsNotSet(value))
                return null;
            return decimal.Parse(value);
        }

        [StepArgumentTransformation]
        public decimal DecimalTransformation(string value)
        {
            if (MapDriver.IsNotSet(value))
                return 0;
            return decimal.Parse(value);
        }
    }
}