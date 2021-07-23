using System;
using SpecFlowTests.Utils.Common.Converter;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Utils.Common.StepArgumentTransformations
{
    [Binding]
    public class StringToNullableDateTimeStepArgumentTransformation
    {
        [StepArgumentTransformation]
        public DateTime? TransformNullableDateTime(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NotSupportedException("'Empty' entry to 'DateTime' transformation is not supported.");
            }

            return DateConverter.GetDateFrom(value);
        }
    }
}