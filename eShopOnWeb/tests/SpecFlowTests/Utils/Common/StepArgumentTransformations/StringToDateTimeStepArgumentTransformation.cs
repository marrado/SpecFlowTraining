using System;
using SpecFlowTests.Utils.Common.Converter;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Utils.Common.StepArgumentTransformations
{
    [Binding]
    public class StringToDateTimeStepArgumentTransformation
    {
        [StepArgumentTransformation]
        public DateTime TransformDateTime(string value)
        {
            if (MapDriver.IsNotSet(value))
                throw new NotSupportedException("'Empty' entry to 'DateTime' transformation is not supported.");

            return DateConverter.GetDateFrom(value).Value;
        }
    }
}
