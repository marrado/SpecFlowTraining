using System;
using SpecFlowTests.Utils.Common.Converter;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Utils.Common.StepArgumentTransformations
{
    [Binding]
    public class StringToDateTimeOffsetStepArgumentTransformation
    {
        [StepArgumentTransformation]
        public DateTimeOffset TransformDateTimeOffset(string value)
        {
            if (MapDriver.IsNotSet(value))
                throw new NotSupportedException("'Empty' entry to 'DateTimeOffset' transformation is not supported.");

            return DateConverter.GetDateOffset(value).Value;
        }
    }
}
