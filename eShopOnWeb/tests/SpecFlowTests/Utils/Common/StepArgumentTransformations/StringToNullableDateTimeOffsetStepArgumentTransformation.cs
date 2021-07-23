using System;
using SpecFlowTests.Utils.Common.Converter;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Utils.Common.StepArgumentTransformations
{
    [Binding]
    public class StringToNullableDateTimeOffsetStepArgumentTransformation
    {
        [StepArgumentTransformation]
        public DateTimeOffset? TransformNullableDateTimeOffset(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NotSupportedException("'Empty' entry to 'DateTimeOffset' transformation is not supported.");
            }

            return DateConverter.GetDateOffset(value);
        }
    }
}
