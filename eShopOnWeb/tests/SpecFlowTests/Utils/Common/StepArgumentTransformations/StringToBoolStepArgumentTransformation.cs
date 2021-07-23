using System;
using TechTalk.SpecFlow;

namespace SpecFlowTests.Utils.Common.StepArgumentTransformations
{
    [Binding]
    public class StringToBoolStepArgumentTransformation
    {
        [StepArgumentTransformation]
        public bool BoolTransform(string result)
        {
            if (string.IsNullOrWhiteSpace(result))
                throw new NotSupportedException("'Empty' entry to 'bool' transformation is not supported.");

            return MapDriver.MapToBool(result);
        }
    }
}
